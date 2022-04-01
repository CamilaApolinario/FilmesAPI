using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class EmailService
    {
        private IConfiguration _configuration;// carregar configurações definidas em nosso appsettings

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void EnviarEmail(string[] destinatario, string assunto, int usuarioId, string code)
        {
            Mensagem mensagem = new Mensagem(destinatario, assunto, usuarioId, code);
            var mensagemDeEmail = CriaCorpoDoEmail(mensagem);// recebe a mensagem criada, e faz as coversões
            Enviar(mensagemDeEmail);
        }

        private void Enviar(MimeMessage mensagemDeEmail)// recebe a mensagem que geramos
        {
            using(var client = new SmtpClient()) //abrir a conexão com servidor através da classe SmtpClient
            {
                try
                {
                    client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"),
                        _configuration.GetValue<int>("EmailSettings:Port"), true);//conecta nosso cliente ao servidor de email//carrega o valor deesa cpnfiguração o Smtp
                    
                    client.AuthenticationMechanisms.Remove("XOUATH2"); 
                    
                    client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"),
                        _configuration.GetValue<string>("EmailSettings:Password"));
                    
                    client.Send(mensagemDeEmail); // envia a mensagem
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);// independente se vamos conseguir fazer o envio, precisamos disconectar do servidor
                    client.Dispose(); //libera os recursos
                }
            }
        }

        private MimeMessage CriaCorpoDoEmail(Mensagem mensagem)//recebe a mensagem faz as conversões necessarias para conseguirmos enviar por email
        {
            var mensagemDeEmail = new MimeMessage();
            mensagemDeEmail.From.Add(new MailboxAddress(_configuration.GetValue<string>("EmailSettings:From"))); //não uma simples string, mas o parametro é um MailboxAddress
            mensagemDeEmail.To.AddRange(mensagem.Destinatario);
            mensagemDeEmail.Subject = mensagem.Assunto;
            mensagemDeEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)// converte para um TextPart, nossa mensagem conteudo
            {
                Text = mensagem.Conteudo
            };
            return mensagemDeEmail;
        }
    }
}