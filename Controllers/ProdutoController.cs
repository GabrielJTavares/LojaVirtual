using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using LojaVirtual.Models.ProdutoAgregador;

namespace LojaVirtual.Controllers
{
    public class ProdutoController : Controller
    {
        private ICategoriaRepository _categoriarepository;
        private IProdutoRepository _produtoRepository;

        public ProdutoController(ICategoriaRepository categoriarepository, IProdutoRepository produtoRepository)
        {
            _categoriarepository = categoriarepository;
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        [Route("/Produto/Categoria/{slug}")]
        public IActionResult ListagemCategoria(string slug)
        {
            
            return View(_categoriarepository.FindCategoryById(slug));
        }
        
        [HttpGet]
        [Route("/Produto/Visualizar")]
        public IActionResult Visualizar(int id)
        {
            Produto produto = _produtoRepository.FindByIdPro(id);
            return View(produto);
        }

        
    }
}