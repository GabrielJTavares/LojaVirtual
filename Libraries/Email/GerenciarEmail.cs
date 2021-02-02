using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using LojaVirtual.Models;
using Microsoft.Extensions.Configuration;

namespace LojaVirtual.Libraries.Email
{
    public class GerenciarEmail
    {
        private SmtpClient _smtp;
        private IConfiguration _configuration;

        public GerenciarEmail(SmtpClient smtp,IConfiguration configuration)
        {
            _smtp = smtp;
            _configuration = configuration;
        }
        public  void EnviarContatoPorEmail(Contato contato)
        {
            

            string corpoMsg = string.Format("<h2> Contato - Careca's Store</h2>" +
            "<b>Nome: </b> {0} <br />" +
            "<b>E-mail: </b> {1} <br />" +
            "<b>Texto: </b> {2} <br />" +
            "Email enviado automaticamente pelo site Careca's Store",
            contato.Nome, contato.Email, contato.Texto);

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:UserName"));
            mensagem.To.Add(contato.Email);
            mensagem.Subject = "Contato - Careca's Store";
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            _smtp.Send(mensagem);


         
        }
        public void EnviarSenhaParaColaboradorEmail(Colaborador colaborador)
        {


            string corpoMsg = string.Format("<h2> Colaborador - Careca's Store</h2>" +
                "Sua Senha é:" +
                "<h3>{0}</h3>", colaborador.Senha);
            

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:UserName"));
            mensagem.To.Add(colaborador.Email);
            mensagem.Subject = "Colaborador - Careca's Store - Senha do Colaborador" + colaborador.Nome;
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            _smtp.Send(mensagem);



        }
    }
}
