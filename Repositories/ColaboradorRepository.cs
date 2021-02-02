using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.DataBase;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LojaVirtual.Repositories
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private IConfiguration _conf;
        LojaVirtualContext _context;
        public ColaboradorRepository(LojaVirtualContext context,IConfiguration configuration)
        {
            _context = context;
            _conf = configuration;
        }
        public void AddColaborador(Colaborador colaborador)
        {
            if (!ExistColaborador(colaborador.Email))
            {
                _context.TAB_Colaboradores.Add(colaborador);
                _context.SaveChanges();
            }
        }

        

        public Colaborador FindByIdColaborador(int id)
        {
           return _context.TAB_Colaboradores.Find(id);
        }

        public Colaborador LoginColaborador(string Email, string senha)
        {
             Colaborador colaborador = _context.TAB_Colaboradores.Where(m => m.Email == Email && m.Senha == senha).FirstOrDefault();
            return colaborador;
        }

        public void Remove(int Id)
        {
            
            _context.TAB_Colaboradores.Remove(FindByIdColaborador(Id));
            _context.SaveChanges();
        }

        public void UpdateColaborador(Colaborador colaborador)
        {
            _context.TAB_Colaboradores.Update(colaborador);
            _context.Entry(colaborador).Property(a => a.Senha).IsModified = false;
            _context.SaveChanges();
        }
        public void UpdatePasswordCol(Colaborador colaborador)
        {
            _context.TAB_Colaboradores.Update(colaborador);
            _context.Entry(colaborador).Property(a => a.Nome).IsModified = false;
            _context.Entry(colaborador).Property(a => a.Email).IsModified = false;
            _context.Entry(colaborador).Property(a => a.Tipo).IsModified = false;
            _context.SaveChanges();
        }

        public bool ExistColaborador(string Email)
        {
           return _context.TAB_Colaboradores.Any(m => m.Email == Email);
        }

        public IPagedList<Colaborador> FindAllColaborador(int? pagina)
        {
            int NumeroPagina = pagina ?? 1;
            return _context.TAB_Colaboradores.Where(a => a.Tipo != "G").ToPagedList<Colaborador>(NumeroPagina, _conf.GetValue<int>("RegistroPorPagina"));
        }

        public List<Colaborador> ObterColaboradorEmail(string email)
        {
            return _context.TAB_Colaboradores.Where(a => a.Email == email).AsNoTracking().ToList();
        }
    }
}
