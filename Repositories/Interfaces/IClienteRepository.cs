using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Cliente LoginClient(string Email, string senha);
        void RegisterClient(Cliente cliente);
        void UpdateClient(Cliente cliente);
        void RemoveClient(int Id);
        Cliente FindClientById(int Id);
        IEnumerable<Cliente> FindAllClient();
        bool ExistsClient(string cpf, string email);
        

    }
}
