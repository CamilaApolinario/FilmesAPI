using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace UsuariosApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //MailMessage mensagemEmail = new MailMessage("aluracursos32@gmail.com", "vinicius.ap@live.com", "teste", "teste");

            //SmtpClient client = new SmtpClient("smtp.gmail.com", 465);
            //client.EnableSsl = true;
            //NetworkCredential cred = new NetworkCredential("aluracursos32@gmail.com", "cursoAlura123@");
            //client.Credentials = cred;

            //// inclui as credenciais
            //client.UseDefaultCredentials = true;

            //// envia a mensagem
            //client.Send(mensagemEmail);


            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureAppConfiguration((context, builder) => builder.AddUserSecrets<Program>());
    }
}
