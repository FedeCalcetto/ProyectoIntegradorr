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
        private readonly IProductoRepositorio _productoRepo;
        public MercadoPagoWebhookController(
            IConfiguration config,
            IOrdenRepositorio ordenRepo,
            IProductoRepositorio productoRepo)
        {
            _config = config;
            _ordenRepo = ordenRepo;
            _productoRepo = productoRepo;
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

            if (Guid.TryParse(ordenId, out var ordenGuid))
            {
                var orden = await _ordenRepo.ObtenerOrdenPorIdAsync(ordenGuid);
                if (orden == null)
                    return;
                switch (status)
                {
                    case "approved":
                        if (orden.Estado != EstadoOrden.Pagada)
                        {
                            orden.MarcarComoPagada(payment.Id.Value);

                            var ordenItems = orden.Items;

                            foreach (var item in ordenItems)
                            {
                                var producto = _productoRepo
                                    .Obtener(item.ProductoId);

                                producto.DescontarStock(item.Cantidad);

                                _productoRepo.Editar(producto);
                            }

                            await _ordenRepo.ActualizarOrdenAsync(orden);
                        }
                        break;

                        case "rejected":
                            orden.MarcarComoRechazada();
                            await _ordenRepo.ActualizarOrdenAsync(orden);
                            break;

                        case "cancelled":
                            orden.MarcarComoCancelada();
                            await _ordenRepo.ActualizarOrdenAsync(orden);
                            break;

                        case "pending":
                        case "in_process":
                        default:
                            orden.MarcarComoPendiente();
                            await _ordenRepo.ActualizarOrdenAsync(orden);
                            break;
                    }

                

            }
        }
    }

}
