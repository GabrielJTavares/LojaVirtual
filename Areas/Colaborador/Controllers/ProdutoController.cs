using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LojaVirtual.Libraries.Arquivo;
using LojaVirtual.Libraries.Filtro;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    [ColaboradorAutorizacao]
    public class ProdutoController : Controller
    {

        private IProdutoRepository _produtoRepository;
        private ICategoriaRepository _categoriaRepository;
        private IImagemRepository _imagemRepository;
        public ProdutoController(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository,IImagemRepository imagemRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _imagemRepository = imagemRepository;
        }
        public IActionResult Index(int? pagina, string pesquisa)
        {
            var produtos =_produtoRepository.FindAllProduto(pagina, pesquisa);
            return View(produtos);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            ViewBag.Categorias = _categoriaRepository.FindAllCategoria().Select(a=> new SelectListItem(a.Nome,a.Id.ToString()));
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _produtoRepository.CreateProduto(produto);

                List<Imagem> ListaCaminhoDef = GerenciadorArquivos.MoverImagensProduto(new List<string>(Request.Form["imagem"]), produto.Id);
                _imagemRepository.CreateImageProduto(ListaCaminhoDef, produto.Id);

                TempData["MSG_S"] = Mensagem.MSG_S001;

                return RedirectToAction(nameof(Index));
            }
            else
            {               
                ViewBag.Categorias = _categoriaRepository.FindAllCategoria().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
                produto.Imagens = new List<string>(Request.Form["imagem"]).Where(a=> a.Trim().Length>0).Select(a => new Imagem() { Caminho = a }).ToList();
                return View(produto);
            }
           
        }
        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            Produto produto = _produtoRepository.FindByIdPro(id);
            ViewBag.Categorias = _categoriaRepository.FindAllCategoria().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View(produto);
        }
        [HttpPost]
        public IActionResult Atualizar(Produto produto,int id)
        {
            if (ModelState.IsValid)
            {
                _produtoRepository.UpdateProduto(produto);

                List<Imagem> ListaCaminhoDef = GerenciadorArquivos.MoverImagensProduto(new List<string>(Request.Form["imagem"]), produto.Id);
                _imagemRepository.DeleteAllImages(produto.Id);
                _imagemRepository.CreateImageProduto(ListaCaminhoDef, produto.Id);

                TempData["MSG_S"] = Mensagem.MSG_S001;

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Categorias = _categoriaRepository.FindAllCategoria().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
                produto.Imagens = new List<string>(Request.Form["imagem"]).Where(a => a.Trim().Length > 0).Select(a => new Imagem() { Caminho = a }).ToList();
                return View(produto);
            }
            
            
        }

        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult Excluir(int id)
        {
            Produto produto=_produtoRepository.FindByIdPro(id);
            GerenciadorArquivos.ExluirImagensProduto(produto.Imagens.ToList());
            _imagemRepository.DeleteAllImages(id);
            _produtoRepository.RemoveProduto(id);

            TempData["MSG_S"] = Mensagem.MSG_S002;
           return RedirectToAction(nameof(Index));
        }

    }
}