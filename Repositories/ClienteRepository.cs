using LojaVirtual.DataBase;
using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Repositories.Interfaces;
using X.PagedList;
using Microsoft.Extensions.Configuration;

namespace LojaVirtual.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private IConfiguration _conf;
        private LojaVirtualContext _context;
        public ClienteRepository(LojaVirtualContext context, IConfiguration configuration)
        {
            _context = context;
            _conf = configuration;
        }


        public IPagedList<Cliente>FindAllClient(int? pagina, string pesquisa)
        {
            int NumeroPagina = pagina ?? 1;

            var bancoCliente = _context.TAB_Clientes.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
            {
                bancoCliente = bancoCliente.Where(a => a.Nome.Contains(pesquisa.ToUpper().Trim()) || a.Email.Contains(pesquisa.ToUpper().Trim()));
            }
            return bancoCliente.ToPagedList<Cliente>(NumeroPagina, _conf.GetValue<int>("RegistroPorPagina")); 
        }

        public Cliente FindClientById(int id)
        {
           return _context.TAB_Clientes.Find(id);
        }

        public Cliente LoginClient(string Email, string senha)
        {
            return _context.TAB_Clientes.Where(m => m.Email == Email && m.Senha == senha).FirstOrDefault();
        }

        public void RegisterClient(Cliente cliente)
        {
            _context.TAB_Clientes.Add(cliente);
            _context.SaveChanges();
        }

        public void RemoveClient(int id)
        {
           
            _context.TAB_Clientes.Remove(FindClientById(id));
            _context.SaveChanges();
        }

        public void UpdateClient(Cliente cliente)
        {
            _context.Update(cliente);
            _context.SaveChanges();
        }
        public bool ExistsClient(string cpf, string email)
        {
            return _context.TAB_Clientes.Any(m => m.CPF == cpf || m.Email == email);
        }
    }
}
