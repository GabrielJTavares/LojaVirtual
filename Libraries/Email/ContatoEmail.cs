using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using LojaVirtual.Models;

namespace LojaVirtual.Libraries.Email
{
    public class ContatoEmail
    {
        public static void EnviarContatoPorEmail(Contato contato)
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("gabriel-tavares78@uni9.edu.br", "bass71163");
            smtp.EnableSsl = true;

            string corpoMsg = string.Format("<h2> Contato - Careca's Store</h2>" +
            "<b>Nome: </b> {0} <br />" +
            "<b>E-mail: </b> {1} <br />" +
            "<b>Texto: </b> {2} <br />" +
            "Email enviado automaticamente pelo site Careca's Store",
            contato.Nome, contato.Email, contato.Texto);

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress("gabriel-tavares78@uni9.edu.br");
            mensagem.To.Add(contato.Email);
            mensagem.Subject = "Contato - Careca's Store";
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            smtp.Send(mensagem);


         
        }
    }
}
