using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        
        void CreateProduto(Produto produto);
        void UpdateProduto(Produto produto);
     
        void RemoveProduto(int id);

        Produto FindByIdPro(int id);
  
        IPagedList<Produto> FindAllProduto(int? pagina, string pesquisa);
 

    }
}
