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
            int NumeroPagina = pagina ?? 1;

            var bancoProduto = _context.TAB_Produto.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
            {
                bancoProduto = bancoProduto.Where(a => a.Nome.Contains(pesquisa.ToUpper().Trim()));
            }
            return bancoProduto.Include(a => a.Imagens).ToPagedList<Produto>(NumeroPagina, _conf.GetValue<int>("RegistroPorPagina"));
        }

        public Produto FindByIdPro(int id)
        {
            return _context.TAB_Produto.Include(a => a.Imagens).Where(a => a.Id==id).FirstOrDefault();
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
