using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ProyectoIntegrador.LogicaAplication.Interface;

namespace ProyectoIntegrador_Web.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task EnviarCodigoAsync(string destino, string codigo, string tipo, string token)
        {
            var from = _config["EmailSettings:From"];
            var password = _config["EmailSettings:Password"];
            var host = _config["EmailSettings:Host"] ?? "smtp.gmail.com";
            var port = int.TryParse(_config["EmailSettings:Port"], out var p) ? p : 587;

            /*using var smtp = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(from, password),
                EnableSsl = true
                smtp.UseDefaultCredentials = false;
            };*/

            using var smtp = new SmtpClient(host, port);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false; // 🔥 NECESARIO PARA GMAIL
            smtp.Credentials = new NetworkCredential(from, password);

            // ----- TEXTO SEGÚN TIPO -----
            string asunto;
            string cuerpo;
            if (tipo == "verificacion")
            {
                // Incluyo token en el link solo si no es null
                string link = token != null
                    ? $"https://localhost:7131/Login/VerificarEmail?email={destino}&token={token}"
                    : $"https://localhost:7131/Login/VerificarEmail?email={destino}";

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


        public async Task EnviarAvisoPedidoAceptadoAsync(
         string destino,
        string nombreArtesano,
         string telefonoArtesano,
        string tituloPedido)
        {
            var from = _config["EmailSettings:From"];
            var password = _config["EmailSettings:Password"];
            var host = _config["EmailSettings:Host"] ?? "smtp.gmail.com";
            var port = int.TryParse(_config["EmailSettings:Port"], out var p) ? p : 587;


            using var smtp = new SmtpClient(host, port);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(from, password);


            var asunto = "Tu pedido fue aceptado 🎉";


            var cuerpo = $@"
            Hola!

            Tu pedido ""{tituloPedido}"" fue aceptado por:
            
            👤 {nombreArtesano}
            📞 {telefonoArtesano}

            Gracias por usar nuestra plataforma 💙
            ";


            var mensaje = new MailMessage();
            mensaje.From = new MailAddress(from);
            mensaje.To.Add(destino);
            mensaje.Subject = asunto;
            mensaje.Body = cuerpo;
            mensaje.IsBodyHtml = false;


            await smtp.SendMailAsync(mensaje);
        }

        public async Task EnviarAvisoPedidoFinalizadoAsync(string destino,string tituloPedido, string nombreArtesano, string email)
        {
            var from = _config["EmailSettings:From"];
            var password = _config["EmailSettings:Password"];
            var host = _config["EmailSettings:Host"] ?? "smtp.gmail.com";
            var port = int.TryParse(_config["EmailSettings:Port"], out var p) ? p : 587;


            using var smtp = new SmtpClient(host, port);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(from, password);

            var asunto = "Tu pedido fue aceptado 🎉";


            var cuerpo = $@"
            Hola!

            Tu pedido ""{tituloPedido}"" echo X {nombreArtesano} fue finalizado!! Contactate con tu Artesano: {email}, para coordinar envio.

            Gracias por usar nuestra plataforma y esperamos que disfrute de su pedido 💙
            ";


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