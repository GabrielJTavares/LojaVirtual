using LojaVirtual.Models;
using LojaVirtual.Models.ProdutoAgregador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Gerenciador.Frete
{
    public class CalcularPacote
    {
        public List<Pacote> CalularPacotesDeProdutos(List<ProdutoItem> produtos)
        {
            List<Pacote> pacotes = new List<Pacote>();

            Pacote pacote = new Pacote();
            foreach(var item in produtos)
            {
                for(int i=0; i < item.QuantidadeProdutoCarrinho; i++)
                {
                    var peso=pacote.Peso+item.Peso;
                    var comprimento = (pacote.Comprimento > item.Comprimento) ? pacote.Comprimento : item.Comprimento ;
                    var laragura = (pacote.Largura > item.Largura) ? pacote.Largura : item.Largura;
                    var altura=pacote.Altura+item.Altura;

                    var dimensao = comprimento + laragura + altura;
                    if(peso>30|| dimensao > 200)
                    {
                        pacotes.Add(pacote);
                        pacote = new Pacote();
                    }

                   pacote.Peso = Convert.ToDouble(pacote.Peso + item.Peso);
                    pacote.Comprimento = Convert.ToInt32((pacote.Comprimento > item.Comprimento) ? pacote.Comprimento : item.Comprimento);
                    pacote.Largura = Convert.ToInt32((pacote.Largura > item.Largura) ? pacote.Largura : item.Largura);
                    pacote.Altura = Convert.ToInt32(pacote.Altura + item.Altura);


                }
            }

            pacotes.Add(pacote);
            return pacotes;
        }
    }
}
