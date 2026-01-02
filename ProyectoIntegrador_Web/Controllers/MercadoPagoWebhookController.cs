using Microsoft.AspNetCore.Mvc;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProyectoIntegrador_Web.Controllers
{
    using MercadoPago.Client.Payment;
    using MercadoPago.Config;
    using MercadoPago.Resource.Payment;
    using ProyectoIntegrador.LogicaNegocio.Entidades;
    using System.Text.Json;

    [ApiController]
    [Route("api/mercadopago")]
    public class MercadoPagoWebhookController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IOrdenRepositorio _ordenRepo;

        public MercadoPagoWebhookController(
            IConfiguration config,
            IOrdenRepositorio ordenRepo)
        {
            _config = config;
            _ordenRepo = ordenRepo;

            MercadoPagoConfig.AccessToken =
                _config["MercadoPago:AccessToken"];
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook([FromBody] JsonElement payload)
        {
            // MP puede mandar distintos eventos
            if (!payload.TryGetProperty("type", out var type) ||
                type.GetString() != "payment")
                return Ok();

            if (!payload.TryGetProperty("data", out var data))
                return Ok();

            var paymentId = data.GetProperty("id").GetString();

            if (string.IsNullOrEmpty(paymentId))
                return Ok();

            await ProcesarPago(paymentId);

            return Ok(); // MP espera 200
        }

        private async Task ProcesarPago(string paymentId)
        {
            var client = new PaymentClient();

            Payment payment = await client.GetAsync(long.Parse(paymentId));

            var status = payment.Status;
            var ordenId = payment.ExternalReference;

            if (status == "approved" &&
                Guid.TryParse(ordenId, out var ordenGuid))
            {
                var orden = await _ordenRepo.ObtenerOrdenPorIdAsync(ordenGuid);

                if (orden == null || orden.Estado == EstadoOrden.Pagada)
                    return;

                orden.MarcarComoPagada();
                await _ordenRepo.ActualizarOrdenAsync(orden);
            }
        }
    }

}
