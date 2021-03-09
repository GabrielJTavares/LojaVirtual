using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LojaVirtual.Libraries.CarrinhoCompra;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Models;
using LojaVirtual.Models.ProdutoAgregador;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    
    public class CarrinhoCompraController : Controller
    {
        private CarrinhoCompra _carrinhoCompra;
        private IProdutoRepository _pordutoRepository;
        private IMapper _mapper;
        public CarrinhoCompraController(CarrinhoCompra carrinhoCompra, IProdutoRepository pordutoRepository, IMapper mapper)
        {
            _pordutoRepository = pordutoRepository;
            _carrinhoCompra = carrinhoCompra;
            _mapper = mapper;
        }
        [Route("/CarrinhoCompra")]
        public IActionResult Index()
        {
            List<ProdutoItem> produtoItemNoCarrinho=_carrinhoCompra.Consultar();
            List<ProdutoItem> produtoItemCompleto = new List<ProdutoItem>();

            foreach(var item in produtoItemNoCarrinho)
            {
                Produto produto =_pordutoRepository.FindByIdPro(item.Id);


                ProdutoItem produtoItem = _mapper.Map<ProdutoItem>(produto);
                produtoItem.QuantidadeProdutoCarrinho = item.QuantidadeProdutoCarrinho;

                produtoItemCompleto.Add(produtoItem);

            }
            return View(produtoItemCompleto);
        }
        [Route("/CarrinhoCompra/AdicionarItem")]
        public IActionResult AdicionarItem(int id)
        {
            Produto produto = _pordutoRepository.FindByIdPro(id);
            if (produto == null)
            {
                // não existe
                return View("NaoExisteItem");
            }
            else
            {
                var item = new ProdutoItem { Id = id, QuantidadeProdutoCarrinho = 1 };
                _carrinhoCompra.Cadastrar(item);

                return RedirectToAction(nameof(Index));
            }


        }
        [Route("/CarrinhoCompra/AlterarQtdItem")]
        public IActionResult AlterarQtdItem(int id, int quantidade)
        {
            Produto produto = _pordutoRepository.FindByIdPro(id);
            if (quantidade < 1)
            {
                return BadRequest(new { mensagem = Mensagem.MSG_E007 });
            }else if(quantidade> produto.Quantidade)
            {
                return BadRequest(new { mensagem = Mensagem.MSG_E008 });
            }
            else
            {
                var item = new ProdutoItem() { Id = id, QuantidadeProdutoCarrinho = quantidade };
                _carrinhoCompra.Atualizar(item);
                return Ok(new { mensagem = Mensagem.MSG_S001 });
            }
           
           
        }
        [Route("/CarrinhoCompra/RemoverItem")]
        public IActionResult RemoverItem(int id)
        {
            _carrinhoCompra.Remover(new ProdutoItem() { Id=id});
            return RedirectToAction(nameof(Index));
        }
    }
}