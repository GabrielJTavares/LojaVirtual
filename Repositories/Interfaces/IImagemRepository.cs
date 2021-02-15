using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories.Interfaces
{
    public interface IImagemRepository
    {
        void CreateImage(Imagem imagem);       

        void RemoveImage(int id);
        void DeleteAllImages(int IdProduto);
        void CreateImageProduto(List<Imagem> imagem, int produtoId);



    }
}
