using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Sessao;
using LojaVirtual.Models;
using Newtonsoft.Json;

namespace LojaVirtual.Libraries.Login
{
    public class LoginCliente
    {
        private string Key = "Login.Cliente";
        private Sessao.Sessao _sessao;
        public LoginCliente(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Cliente cliente)
        {
            string clienteString = JsonConvert.SerializeObject(cliente);
            _sessao.Cadastrar(Key, clienteString);
        }

        public Cliente GetClient()
        {
            if (_sessao.Existe(Key))
            {
                string clienteString = _sessao.Consultar(Key);
                return JsonConvert.DeserializeObject<Cliente>(clienteString);
            }
            return null;
            

        }
        public void Logout()
        {
            _sessao.RemoverTodos();
        }
    }
}
