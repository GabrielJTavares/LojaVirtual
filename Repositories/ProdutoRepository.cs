using LojaVirtual.DataBase;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        IConfiguration _conf;
        LojaVirtualContext _context;

        public ProdutoRepository(LojaVirtualContext context, IConfiguration conf)
        {
            _context = context;
            _conf = conf;


        }
        public void CreateProduto(Produto produto)
        {
            _context.TAB_Produto.Add(produto);
            _context.SaveChanges();
        }

        public IPagedList<Produto> FindAllProduto(int? pagina, string pesquisa)
        {
            return FindAllProduto(pagina, pesquisa, "A",null);
        }

        public IPagedList<Produto> FindAllProduto(int? pagina, string pesquisa, string ordenacao,IEnumerable<Categoria> categorias)
        {
            int NumeroPagina = pagina ?? 1;

            var bancoProduto = _context.TAB_Produto.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
            {
                bancoProduto = bancoProduto.Where(a => a.Nome.Contains(pesquisa.ToUpper().Trim()));
            }

            if (ordenacao == "A"){
                bancoProduto = bancoProduto.OrderBy(a => a.Nome);
            }
            if (ordenacao == "ME")
            {
                bancoProduto = bancoProduto.OrderBy(a => a.Valor);
            }
            if (ordenacao == "MA")
            {
                bancoProduto = bancoProduto.OrderByDescending(a => a.Valor);
            }

            if(categorias!=null && categorias.Count() > 0)
            {
               
                bancoProduto = bancoProduto.Where(a => categorias.Select(b => b.Id).Contains(a.CategoriaId));
            }
           
            return bancoProduto.Include(a => a.Imagens).ToPagedList<Produto>(NumeroPagina, _conf.GetValue<int>("RegistroPorPagina"));
        }

        public Produto FindByIdPro(int id)
        {
            return _context.TAB_Produto.Include(a => a.Imagens).OrderBy(a=>a.Nome).Where(a => a.Id==id).FirstOrDefault();
        }

        public void RemoveProduto(int id)
        {
            _context.TAB_Produto.Remove(FindByIdPro(id));
            _context.SaveChanges();
        }

        public void UpdateProduto(Produto produto)
        {
            _context.Update(produto);
            _context.SaveChanges();
        }
    }
}
