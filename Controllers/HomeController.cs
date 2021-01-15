using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LojaVirtual.Models;
using LojaVirtual.Libraries.Email;

using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        private object contatoEmail;

        public IActionResult Index()
        {
            return View();
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

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CadastroUsuario()
        {
            return View();
        }

        public IActionResult CarrinhoCompra()
        {
            return View();
        }
    }
}