using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Arquivo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class ImagemController : Controller
    {
        [HttpPost]
        public IActionResult Armazenar(IFormFile file)
        {
            var Caminho = GerenciadorArquivos.CadastrarImagemProduto(file);
            if (Caminho.Length > 0)
            {
                return Ok(new { caminho = Caminho });
            }
            else
            {
                return new StatusCodeResult(500);
            }

        }
        public IActionResult Deletar(string caminho)
        {
            if (GerenciadorArquivos.ExluirImagemProduto(caminho))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
    }
}