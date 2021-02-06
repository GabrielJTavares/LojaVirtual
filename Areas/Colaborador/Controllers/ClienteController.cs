using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Filtro;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Models;
using LojaVirtual.Models.Constantes;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    [ColaboradorAutorizacao]
    public class ClienteController : Controller
    {
        private IClienteRepository _clienteRepository;
        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public IActionResult Index(int? pagina,string pesquisa)
        {
            IPagedList<Cliente> clientes = _clienteRepository.FindAllClient(pagina, pesquisa);
            return View(clientes);
        }
        [ValidateHttpReferer]
        public IActionResult AtivarDesativar(int id)
        {
            Cliente cliente = _clienteRepository.FindClientById(id);

            cliente.Situacao=(cliente.Situacao == SituacaoCliente.Ativo)? cliente.Situacao = SituacaoCliente.Desativado : cliente.Situacao = SituacaoCliente.Ativo;

            _clienteRepository.UpdateClient(cliente);

            TempData["MSG_S"] = Mensagem.MSG_S001;

            return RedirectToAction(nameof(Index));
        }

    }
}