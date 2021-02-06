using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Cliente LoginClient(string Email, string senha);
        void RegisterClient(Cliente cliente);
        void UpdateClient(Cliente cliente);
        void RemoveClient(int Id);
        Cliente FindClientById(int Id);
        IPagedList<Cliente> FindAllClient(int? pagina,string pesquisa);
        bool ExistsClient(string cpf, string email);

        

    }
}
