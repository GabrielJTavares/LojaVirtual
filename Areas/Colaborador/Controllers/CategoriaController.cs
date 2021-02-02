using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Filtro;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]

    [ColaboradorAutorizacao]
    public class CategoriaController : Controller
    {

        public ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {

            _categoriaRepository = categoriaRepository;
        }
        public IActionResult Index(int?pagina)
        {
            var categorias = _categoriaRepository.FindAllCategori(pagina);
            return View(categorias);
        
        }
        [HttpGet]
        public IActionResult Cadastrar()
        {
            ViewBag.Categorias = _categoriaRepository.FindAllCategoria().Select(a=>new SelectListItem(a.Nome,a.Id.ToString()));
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar([FromForm]Categoria categoria)
        {//TODO - Implementar
            if (ModelState.IsValid)
            {
                _categoriaRepository.RegisterRegistrar(categoria);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categorias = _categoriaRepository.FindAllCategoria().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View();
           
        }
        [HttpGet]
        public IActionResult Atualizar(int Id)
        {
            var categoria = _categoriaRepository.FindCategoryById(Id);
            ViewBag.Categorias = _categoriaRepository.FindAllCategoria().Where(a=>a.Id!= Id).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View(categoria);
        }
        [HttpPost]
        public IActionResult Atualizar([FromForm]Categoria categoria,int id)
        {
            if (ModelState.IsValid)
            {
                
                _categoriaRepository.UpdateCategoria(categoria);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.Categorias = _categoriaRepository.FindAllCategoria().Where(a => a.Id != id).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));

            return View();
        }

        [HttpGet]
        public IActionResult Excluir(int Id)
        {
            _categoriaRepository.RemoveCategoria(Id);
            TempData["MSG_S"] = Mensagem.MSG_S002;
            return RedirectToAction(nameof(Index));
        }

    }
}