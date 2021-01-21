using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LojaVirtual.Models;
using LojaVirtual.Libraries.Email;

using System.ComponentModel.DataAnnotations;
using System.Text;
using LojaVirtual.DataBase;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using LojaVirtual.Libraries.Login;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        private IClienteRepository _ClienteRepository;
        private INewsletterRepository _newsletterRepository;
        private LoginCliente _loginCliente;

        public HomeController(IClienteRepository repository,INewsletterRepository newletter, LoginCliente logincliente)
        {
            _ClienteRepository = repository;
            _newsletterRepository = newletter;
            _loginCliente = logincliente;


        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index([FromForm]NewsletterEmail newsletter)
        {
            if (ModelState.IsValid)
            {

                if (!_newsletterRepository.existNewsletter(newsletter.Email))
                {
                    _newsletterRepository.AddNewsletter(newsletter);

                    TempData["MSG_S"] = "E-mail cadastrado!";
                }
                else
                {
                    TempData["MSG_E"] = "E-mail já existe!";
                }
                
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
           
        }

        public IActionResult Contato()
        {
            return View();
        }

        public IActionResult ContatoAcao()
        {
            try
            {
                Contato contato = new Contato();
                contato.Nome = HttpContext.Request.Form["nome"];
                contato.Email = HttpContext.Request.Form["email"];
                contato.Texto = HttpContext.Request.Form["texto"];

                var listaMensagens = new List<ValidationResult>();
                var contexto = new ValidationContext(contato);
                bool isValid = Validator.TryValidateObject(contato, contexto, listaMensagens, true);

                

                if (isValid) {
                    ContatoEmail.EnviarContatoPorEmail(contato);
                    ViewData["MSG_S"] = "Mensagem de contato enviado com sucesso!";
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var texto in listaMensagens)
                    {
                        sb.Append(texto.ErrorMessage+"<br />");
                    }
                    ViewData["MSG_E1"] = sb.ToString();
                    ViewData["CONTATO"] = contato;
                }

               
            }
            catch(Exception e)
            {
                ViewData["MSG_E1"] = "OPPS! Tivemos um erro, tente novamente mais tarde";
                //TODO - Implementar LOG
            }
           
            return View("Contato");
            }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([FromForm] Cliente cliente)
        {
            Cliente clienteDB = _ClienteRepository.LoginClient(cliente.Email, cliente.Senha);
            if (clienteDB!=null)
            {
                _loginCliente.Login(clienteDB);
                    return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else
            {
                ViewData["MSG_E1"] = "Usuário não encontrado, verifique email e senha digitado!";
                return View();
            }
          
        }
        [HttpGet]
        public IActionResult Painel()
        {
            Cliente cliente = _loginCliente.GetClient();
            if (cliente!=null)
            {
                return new ContentResult() { Content = "Usuário " + cliente.Id + ", Nome: "+cliente.Nome+" Logado" };
            }
            else
            {
                return new ContentResult() { Content = "Deu Ruim" };
            }
        }


        [HttpGet]
        public IActionResult CadastroUsuario()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult CadastroUsuario([FromForm] Cliente cliente)
        {

            if (ModelState.IsValid)
            {
                if (!_ClienteRepository.ExistsClient(cliente.CPF,cliente.Email))
                {
                    _ClienteRepository.RegisterClient(cliente);
                    
                    TempData["MSG_S"]="Cadastro feito com sucesso!!!";
                   return RedirectToAction(nameof(CadastroUsuario));
                }
                else
                {
                    TempData["MSG_E"] = "Cadastro já existe!!!";
                    return View();
                }

            }
           
            return View();

        }

        public IActionResult CarrinhoCompra()
        {
            return View();
        }
    }
}