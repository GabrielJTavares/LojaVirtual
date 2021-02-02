using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Models;
using X.PagedList;

namespace LojaVirtual.Repositories.Interfaces
{
   public interface ICategoriaRepository
    {
    
        void RegisterRegistrar(Categoria categoria);
        void UpdateCategoria(Categoria categoria);
        void RemoveCategoria(int Id);
        Categoria FindCategoryById(int Id);
        IEnumerable<Categoria> FindAllCategoria();
        IPagedList<Categoria> FindAllCategori(int? pagina);
       
    }
}
