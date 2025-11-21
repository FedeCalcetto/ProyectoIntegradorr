using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProyectoIntegrador_Web.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task EnviarCodigoAsync(string destino, string codigo)
        {
            var from = _config["EmailSettings:From"];
            var password = _config["EmailSettings:Password"];
            var host = _config["EmailSettings:Host"] ?? "smtp.gmail.com";
            var port = int.TryParse(_config["EmailSettings:Port"], out var p) ? p : 587;

            using var smtp = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(from, password),
                EnableSsl = true
            };

            // URL para que el usuario pueda verificar cuando quiera
            string link = $"https://localhost:7131/Login/VerificarEmail?email={destino}";

            string cuerpo = $@"
Tu código de verificación es: {codigo}

Podés verificar tu cuenta haciendo clic en el siguiente enlace o copiá y pegá esta URL en tu navegador:

{link}
";

            var mensaje = new MailMessage();
            mensaje.From = new MailAddress(from);
            mensaje.To.Add(destino);
            mensaje.Subject = "Código de verificación";
            mensaje.Body = cuerpo;
            mensaje.IsBodyHtml = false; // si querés HTML me avisás

            await smtp.SendMailAsync(mensaje);
        }
    }
}