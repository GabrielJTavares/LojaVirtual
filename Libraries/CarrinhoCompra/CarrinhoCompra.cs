using LojaVirtual.Models.ProdutoAgregador;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.CarrinhoCompra
{
    public class CarrinhoCompra
    {
        private string Key = "Carrinho.Compras";
        private Cookie.Cookie _cookie;
        public CarrinhoCompra(Cookie.Cookie cookie)
        {
            _cookie = cookie;
        }
        public void Cadastrar(ProdutoItem item)
        {
            List<ProdutoItem> Lista;
            if (_cookie.Existe(Key))
            {
                Lista = Consultar();
                var IntemLocalizado = Lista.SingleOrDefault(a => a.Id == item.Id);
                if (IntemLocalizado==null)
                {

                    Lista.Add(item);
                }
                else
                {
                    IntemLocalizado.QuantidadeProdutoCarrinho +=1;
                }
                
            }
            else
            {
                 Lista = new List<ProdutoItem>();
                Lista.Add(item);
            }
            Salvar(Lista);
        }

        public void Atualizar(ProdutoItem item)
        {
            var Lista = Consultar();
            var IntemLocalizado = Lista.SingleOrDefault(a => a.Id == item.Id);
            if (IntemLocalizado != null)
            {
                IntemLocalizado.QuantidadeProdutoCarrinho = item.QuantidadeProdutoCarrinho;
                Salvar(Lista);
            }

        }
        public void Remover(ProdutoItem item)
        {
            var Lista = Consultar();
            var IntemLocalizado = Lista.SingleOrDefault(a => a.Id == item.Id);

            if (IntemLocalizado != null)
            {
                Lista.Remove(IntemLocalizado);
                Salvar(Lista);
            }
        }
        public List<ProdutoItem> Consultar()
        {
            if (_cookie.Existe(Key))
            {
                string valor = _cookie.Consultar(Key);
                return JsonConvert.DeserializeObject<List<ProdutoItem>>(valor);
            }
            else
            {
                return new List<ProdutoItem>();
            }

        }

        public void Salvar(List<ProdutoItem> lista)
        {
            string Valor = JsonConvert.SerializeObject(lista);
            _cookie.Cadastrar(Key, Valor);
        }

        public bool Existe(string key)
        {
            if (_cookie.Existe(Key))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void RemoverTodos()
        {

            _cookie.Remover(Key);
        }
    }
 
}
