using LojaVirtual.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;

namespace LojaVirtual.Repositories
{
    public class NewsletterRepository : INewsletterRepository
    {
        private LojaVirtualContext _context;
        public NewsletterRepository(LojaVirtualContext context)
        {
            _context = context;
        }

        public void AddNewsletter(NewsletterEmail newsletter)
        {
            _context.TAB_NewLetterEmails.Add(newsletter);
            _context.SaveChanges();
        }

        public bool existNewsletter(string email)
        {
           return _context.TAB_NewLetterEmails.Any(m => m.Email == email);
        }

        public IEnumerable<NewsletterEmail> FindAllNewsLetter()
        {
           return _context.TAB_NewLetterEmails.ToList();
        }
    }
}
