using LojaVirtual.Models;
using LojaVirtual.Models.ProdutoAgregador;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.DataBase
{
    public class LojaVirtualContext : DbContext
    {
        public LojaVirtualContext(DbContextOptions<LojaVirtualContext> options): base(options)
        {
        }
        public DbSet<Cliente> TAB_Clientes { get; set; }
        public DbSet<NewsletterEmail> TAB_NewLetterEmails { get; set; }
        public DbSet<Colaborador> TAB_Colaboradores { get; set; }
        public DbSet<Categoria> TAB_categorias { get; set; }

        public DbSet<Produto> TAB_Produto { get; set; }
        public DbSet<Imagem> TAB_Imagens { get; set; }


    }
}
