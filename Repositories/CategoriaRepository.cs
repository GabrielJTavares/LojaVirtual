using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.DataBase;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private IConfiguration _conf;
        private LojaVirtualContext _context;
        public CategoriaRepository(LojaVirtualContext context, IConfiguration configuration)
        {
            _context = context;
            _conf = configuration;
        }

     
        public IPagedList<Categoria> FindAllCategori(int? pagina)
        {
            int NumeroPagina = pagina ?? 1;
            return _context.TAB_categorias.Include(a=>a.CategoriaPai).ToPagedList<Categoria>(NumeroPagina, _conf.GetValue<int>("RegistroPorPagina"));
        }

        public IEnumerable<Categoria> FindAllCategoria()
        {
            return _context.TAB_categorias;
        }

        public Categoria FindCategoryById(int Id)
        {
            return _context.TAB_categorias.Find(Id);
        }

        public Categoria FindCategoryById(string slug)
        {
            return _context.TAB_categorias.Where(a => a.Slug == slug).FirstOrDefault();
        }

        private List<Categoria> categorias;
        
        private List<Categoria> ListaCategoriaRecursiva = new List<Categoria>();
        public IEnumerable<Categoria> ObterCategoriaRecursiva(Categoria categoriaPai)
        {
            if(categorias == null)
            {
                categorias = FindAllCategoria().ToList();
            }
           
            if (!ListaCategoriaRecursiva.Exists(a => a.Id == categoriaPai.Id))
            {
                ListaCategoriaRecursiva.Add(categoriaPai);
            }

            var listaCategoriaFilho = categorias.Where(a => a.CartegoriaPaiId == categoriaPai.Id);
            if (listaCategoriaFilho.Count() > 0)
            {
                ListaCategoriaRecursiva.AddRange(listaCategoriaFilho.ToList());
                foreach (var categoria in listaCategoriaFilho)
                {
                    ObterCategoriaRecursiva(categoria);
                }
            }
            return ListaCategoriaRecursiva;
        }
       
     

        public void RegisterRegistrar(Categoria categoria)
        {
            _context.TAB_categorias.Add(categoria);
            _context.SaveChanges();
        }

        public void RemoveCategoria(int Id)
        {
            _context.TAB_categorias.Remove(FindCategoryById(Id));
            _context.SaveChanges();
        }

        public void UpdateCategoria(Categoria categoria)
        {
            _context.Update(categoria);
            _context.SaveChanges();
        }
    }
}
