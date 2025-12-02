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

        public async Task EnviarCodigoAsync(string destino, string codigo, string tipo)
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

            // ----- TEXTO SEGÚN TIPO -----
            string asunto;
            string cuerpo;

            if (tipo == "verificacion")
            {
                string link = $"https://localhost:7131/Login/VerificarEmail?email={destino}";

                asunto = "Código de verificación";
                cuerpo = $@"
Tu código de verificación es: {codigo}

También podés verificar tu cuenta entrando a:
{link}
";
            }
            else if (tipo == "eliminacion")
            {
                asunto = "Código para eliminar tu cuenta";
                cuerpo = $@"
Recibimos tu solicitud para eliminar tu cuenta.

Tu código para confirmar la eliminación es: {codigo}

⚠️ IMPORTANTE
Si tú no pediste esto, no compartas este código con nadie.
";
            }
            else
            {
                asunto = "Código";
                cuerpo = "Tu código es: " + codigo;
            }

            //-------------------------------------------------------

            var mensaje = new MailMessage();
            mensaje.From = new MailAddress(from);
            mensaje.To.Add(destino);
            mensaje.Subject = asunto;
            mensaje.Body = cuerpo;
            mensaje.IsBodyHtml = false;

            await smtp.SendMailAsync(mensaje);
        }
    }
}