using LojaVirtual.DataBase;
using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Repositories.Interfaces;

namespace LojaVirtual.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private LojaVirtualContext _context;
        public ClienteRepository(LojaVirtualContext context)
        {
            _context = context;
        }


        public IEnumerable<Cliente> FindAllClient()
        {
            return _context.TAB_Clientes.ToList(); 
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
