using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Email;
using LojaVirtual.Libraries.Filtro;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Libraries.Texto;
using LojaVirtual.Models.Constantes;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    [ColaboradorAutorizacao(ColaboradorTipoConstant.Gerente)]
    public class ColaboradorController : Controller
    {
        private IColaboradorRepository _colaboradorRepository;
        private GerenciarEmail _gerenciarEmail;
        public ColaboradorController (IColaboradorRepository colaboradorRepository,GerenciarEmail gerenciarEmail)
        {
            _gerenciarEmail = gerenciarEmail;
            _colaboradorRepository = colaboradorRepository;
        }
        public IActionResult Index( int? pagina)
        {
           
            return View(_colaboradorRepository.FindAllColaborador(pagina));
        }
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar([FromForm]Models.Colaborador colaborador)
        {
            ModelState.Remove("Senha");
            ModelState.Remove("ConfirmacaoSenha");
            if (ModelState.IsValid)
            {

                colaborador.Tipo = ColaboradorTipoConstant.Comum;
                colaborador.Senha = KeyGenerator.GetUniqueKey(8);
                
                _colaboradorRepository.AddColaborador(colaborador);
                _gerenciarEmail.EnviarSenhaParaColaboradorEmail(colaborador);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                
               return RedirectToAction(nameof(Index));
                
            }
            return View();
        }

        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult GerarSenha (int id)
        {
           Models.Colaborador colaborador= _colaboradorRepository.FindByIdColaborador(id);
            colaborador.Senha=KeyGenerator.GetUniqueKey(8);
            _colaboradorRepository.UpdatePasswordCol(colaborador);
            _gerenciarEmail.EnviarSenhaParaColaboradorEmail(colaborador);

            TempData["MSG_S"] = Mensagem.MSG_S003;

            return RedirectToAction(nameof(Index));




        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            return View(_colaboradorRepository.FindByIdColaborador(id));
        }
        [HttpPost]
        public IActionResult Atualizar([FromForm]Models.Colaborador colaborador,int id)
        {
            ModelState.Remove("Senha");
            ModelState.Remove("ConfirmacaoSenha");

            if (ModelState.IsValid)
            {
                _colaboradorRepository.UpdateColaborador(colaborador);
                TempData["MSG_S"] = Mensagem.MSG_S001;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult Excluir(int id)
        {
            _colaboradorRepository.Remove(id);
            TempData["MSG_S"] = Mensagem.MSG_S002;

            return RedirectToAction(nameof(Index));
           
        }
    }
}