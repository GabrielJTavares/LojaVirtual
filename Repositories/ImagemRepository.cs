using LojaVirtual.DataBase;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories
{
    public class ImagemRepository : IImagemRepository
    {
        LojaVirtualContext _context;

        public ImagemRepository(LojaVirtualContext context)
        {
            _context = context;

        }
        public void CreateImageProduto(List<Imagem> imagem,int produtoId )
        {
            if (imagem != null && imagem.Count>0)
            {

                foreach (var CaminhoDef in imagem)
                {

                    CreateImage(CaminhoDef);
                }
            }
           
        }
        public void CreateImage(Imagem imagem)
        {
            _context.TAB_Imagens.Add(imagem);
            _context.SaveChanges();
        }

        public void RemoveImage(int id)
        {
            _context.TAB_Imagens.Remove(_context.TAB_Imagens.Find(id));
            _context.SaveChanges();
        }
        public void DeleteAllImages(int IdProduto)
        {
            List<Imagem> imagens = _context.TAB_Imagens.Where(a => a.ProdutoId == IdProduto).ToList();
            foreach(Imagem imagem in imagens)
            {
                _context.TAB_Imagens.Remove(imagem);
            }
           
            _context.SaveChanges();
        }
    }
}
