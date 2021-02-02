using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories.Interfaces
{
    public interface IColaboradorRepository
    {
        Colaborador LoginColaborador(string Email, String senha);
        void AddColaborador(Colaborador colaborador);
        void UpdateColaborador(Colaborador colaborador);
        void UpdatePasswordCol(Colaborador colaborador);
        void Remove(int Id);
        
        Colaborador FindByIdColaborador(int id);
        bool ExistColaborador(string Email);
        IPagedList<Colaborador> FindAllColaborador(int? pagina);
        List<Colaborador> ObterColaboradorEmail(string email);






    }
}
