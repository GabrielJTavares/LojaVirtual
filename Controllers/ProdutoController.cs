using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LojaVirtual.Models;

namespace LojaVirtual.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Visualizar()
        {
            Produto produto = GetProduto();
            return View(produto);
        }

        private Produto GetProduto()
        {
            return new Produto
            {
                Id = 1,
                Nome = "Polystation",
                Descricao = "Não é Playstation",
                Valor = 2000.00M

            };

        }
    }
}