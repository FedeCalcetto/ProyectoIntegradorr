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

            var mensaje = new MailMessage();
            mensaje.From = new MailAddress(from);
            mensaje.To.Add(destino);
            mensaje.Subject = "Código de verificación";
            mensaje.Body = $"Tu código de verificación es: {codigo}";
            mensaje.IsBodyHtml = false;

            await smtp.SendMailAsync(mensaje);
        }
    }
}