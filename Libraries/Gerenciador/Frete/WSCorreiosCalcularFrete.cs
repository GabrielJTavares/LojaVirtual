
using LojaVirtual.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSCorreios;

namespace LojaVirtual.Libraries.Gerenciador.Frete
{
    public class WSCorreiosCalcularFrete
    {
        private IConfiguration _configuration;
        private CalcPrecoPrazoWSSoap _wsCorreios;
        public WSCorreiosCalcularFrete(IConfiguration configuration, CalcPrecoPrazoWSSoap wsCorreios)
        {
            _configuration = configuration;
            _wsCorreios = wsCorreios;
        }

        public async Task<List<ValorPrazoFrete>> CalcularFrete(string cepDestino, string TipoFrete, List<Pacote> pacotes)
        {
            List<ValorPrazoFrete> valorPacotes = new List<ValorPrazoFrete>();

            foreach(var pacote in pacotes)
            {
                valorPacotes.Add( await CalcularValorPrazoFrete(cepDestino, TipoFrete, pacote));
            }

            List<ValorPrazoFrete> valorFretes = valorPacotes
                .GroupBy(a => a.TipoFrete)
                .Select(list=>new ValorPrazoFrete { 
                TipoFrete=list.First().TipoFrete,
                Prazo=list.Max(c=>c.Prazo),
                Valor=list.Sum(c=>c.Valor)
                }).ToList();

            return valorFretes;
        }

        private async Task<ValorPrazoFrete> CalcularValorPrazoFrete(string cepDestino, string TipoFrete, Pacote pacote)
        {
            var cepOrigem=_configuration.GetValue<string>("Frete:CepOrigem");
            var maoPropria=_configuration.GetValue<string>("Frete:MaoPropria");
            var avisoRecebimento=_configuration.GetValue<string>("Frete:AvisoRecebimento");
            var diamentro = Math.Max(Math.Max(pacote.Comprimento, pacote.Largura), pacote.Altura);
           
            cResultado resultado = await _wsCorreios.CalcPrecoPrazoAsync("", "", TipoFrete, cepOrigem, cepDestino, pacote.Peso.ToString(), 1, pacote.Comprimento, pacote.Altura, pacote.Largura, diamentro, maoPropria, 0, avisoRecebimento);

            if (resultado.Servicos[0].Erro == "0")
            {
                //ok
                return new ValorPrazoFrete()
                {
                    TipoFrete=TipoFrete,
                    Prazo = int.Parse(resultado.Servicos[0].PrazoEntrega),
                    Valor=double.Parse(resultado.Servicos[0].Valor.Replace(".","").Replace(",","."))
                };
            }
            else
            {
                throw new Exception("Erro: " + resultado.Servicos[0].MsgErro);
            }
        }
    }
}
