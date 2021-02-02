using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Models;
using Newtonsoft.Json;

namespace LojaVirtual.Libraries.Login
{
    public class LoginColaborador
    {
        private string Key = "Login.Colaborador";
        private Sessao.Sessao _sessao;
        public LoginColaborador(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login( Colaborador colaborador)
        {
            string colaboradorString = JsonConvert.SerializeObject(colaborador);
            _sessao.Cadastrar(Key, colaboradorString);
        }

        public Colaborador GetColaborador()
        {
            if (_sessao.Existe(Key))
            {
                string colaboradorString = _sessao.Consultar(Key);
                return JsonConvert.DeserializeObject<Colaborador>(colaboradorString);
            }
            return null;


        }
        public void Logout()
        {
            _sessao.RemoverTodos();
        }
    }
}
