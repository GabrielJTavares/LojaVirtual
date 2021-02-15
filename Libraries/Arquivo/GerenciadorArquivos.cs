using LojaVirtual.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Arquivo
{
    public class GerenciadorArquivos
    {
        public static string CadastrarImagemProduto(IFormFile file)
        {
            var NomeArquivo = Path.GetFileName(file.FileName);
            var caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/temp", NomeArquivo);

            using (var stream = new FileStream(caminho, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return Path.Combine("/Uploads/temp", NomeArquivo).Replace("\\", "/");


        }
        public static bool ExluirImagemProduto(string caminho)
        {
            string Caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", caminho.TrimStart('/'));
            if (File.Exists(Caminho))
            {
                File.Delete(Caminho);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<Imagem> MoverImagensProduto(List<string> listaCaminhoTemp, int produtoId)
        {
            var caminhoDefinitivoPastaProduto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", produtoId.ToString());

            if (!Directory.Exists(caminhoDefinitivoPastaProduto))
            {
                Directory.CreateDirectory(caminhoDefinitivoPastaProduto);
            }

            List<Imagem> ListaCaminhoDef = new List<Imagem>();
            foreach (var caminhoTemp in listaCaminhoTemp)
            {
                var nomeArquivo = Path.GetFileName(caminhoTemp);
                if (!string.IsNullOrEmpty(caminhoTemp))
                {



                    var caminhoDef = Path.Combine("/uploads", produtoId.ToString(), nomeArquivo).Replace("\\", "/");

                    if (caminhoDef != caminhoTemp)
                    {




                        var caminhoAbsolutoTemp = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temp", nomeArquivo);
                        var CaminhoAbsolutoDef = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", produtoId.ToString(), nomeArquivo);




                        if (File.Exists(caminhoAbsolutoTemp))
                        {


                            //Deleta arquivo do caminho de destino
                            if (File.Exists(CaminhoAbsolutoDef))
                            {
                                File.Delete(CaminhoAbsolutoDef);
                            }
                            //copia arquivo para o caminho de destino
                            File.Copy(caminhoAbsolutoTemp, CaminhoAbsolutoDef);
                            //Deleta o arquivo do caminho de destino
                            if (File.Exists(CaminhoAbsolutoDef))
                            {
                                File.Delete(caminhoAbsolutoTemp);
                            }
                            ListaCaminhoDef.Add(new Imagem() { Caminho = Path.Combine("/uploads", produtoId.ToString(), nomeArquivo).Replace("\\", "/"), ProdutoId = produtoId });
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        ListaCaminhoDef.Add(new Imagem() { Caminho = Path.Combine("/uploads", produtoId.ToString(), nomeArquivo).Replace("\\", "/"), ProdutoId = produtoId });

                    }
                }



            }
            return ListaCaminhoDef;


        }

        public static void ExluirImagensProduto(List<Imagem> list)
        {
            int produtoId=0;
            foreach(var Imagem in list)
            {
                ExluirImagemProduto(Imagem.Caminho);
                produtoId = Imagem.Id;
            }
            var pastaProduto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", produtoId.ToString());
            if (Directory.Exists(pastaProduto))
            {
                Directory.Delete(pastaProduto);
            }
            
        }
    }
}
