using MercadoPago.Client.Payment;
using MercadoPago.Config;
using Microsoft.AspNetCore.Mvc;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador_Web.Services;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProyectoIntegrador_Web.Controllers
{
    using MercadoPago.Client.Payment;
    using MercadoPago.Config;
    using MercadoPago.Resource.Payment;
    using ProyectoIntegrador.LogicaAplication.Interface;
    using ProyectoIntegrador_Web.Services;
    using System.Text.Json;

    [ApiController]
    [Route("api/mercadopago")]
    public class MercadoPagoWebhookController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IProcesarOrdenService _pagoService;
        private readonly IObtenerOrden _obtenerOrden;

        public MercadoPagoWebhookController(
            IConfiguration config,
            IProcesarOrdenService procesarPagosService,
            IObtenerOrden obtenerOrden)
        {
            _config = config;
            _pagoService = procesarPagosService;
            _obtenerOrden = obtenerOrden;
            MercadoPagoConfig.AccessToken =
                _config["MercadoPago:AccessToken"];
            //
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
                var orden = await _obtenerOrden.ObtenerOrdenPorIdAsync(ordenGuid);
                if (orden == null) return;
                switch (status)
                {
                    case "approved":
                        if (!payment.Id.HasValue)
                            return; await _pagoService.ProcesarOrdenPagadaAsync(orden, payment.Id.Value);
                        break;
                    case "rejected":
                        await _pagoService.ProcesarOrdenRechazadaAsync(orden);
                        break;
                    case "cancelled":
                        await _pagoService.ProcesarOrdenCanceladaAsync(orden);
                        break;
                    case "pending":
                    case "in_process":
                    default:
                        await _pagoService.ProcesarOrdenPendienteAsync(orden);
                        break;
                }
            }
        }
    }
    }


//[ApiController]
//[Route("api/mercadopago")]
//public class MercadoPagoWebhookController : ControllerBase
//{
//    private readonly IConfiguration _config;
//    private readonly IProcesarOrdenService _pagoService;
//    private readonly IObtenerOrden _obtenerOrden;

//    public MercadoPagoWebhookController(
//        IConfiguration config,
//        IProcesarOrdenService procesarPagosService,
//        IObtenerOrden obtenerOrden)
//    {
//        _config = config;
//        _pagoService = procesarPagosService;
//        _obtenerOrden = obtenerOrden;
//        MercadoPagoConfig.AccessToken =
//            _config["MercadoPago:AccessToken"];
//    }

//    [HttpPost("webhook")]
//    public async Task<IActionResult> Webhook([FromBody] JsonElement payload)
//    {
//        // MP puede mandar distintos eventos
//        if (!payload.TryGetProperty("type", out var type) ||
//            type.GetString() != "payment")
//            return Ok();

//        if (!payload.TryGetProperty("data", out var data))
//            return Ok();

//        var paymentId = data.GetProperty("id").GetString();

//        if (string.IsNullOrEmpty(paymentId))
//            return Ok();

//        await ProcesarPago(paymentId);

//        return Ok(); // MP espera 200
//    }

//    private async Task ProcesarPago(string paymentId)
//    {

//        var client = new PaymentClient();
//        var payment = await client.GetAsync(long.Parse(paymentId));

//        if (!payment.Id.HasValue)
//            return;

//        var ordenId = payment.ExternalReference;
//        if (!Guid.TryParse(ordenId, out var ordenGuid))
//            return;

//        var orden = await _obtenerOrden.ObtenerOrdenPorIdAsync(ordenGuid);
//        if (orden == null)
//            return;

//        switch (payment.Status)
//        {
//            case "approved":
//                await _pagoService.ProcesarOrdenPagadaAsync(
//                    orden,
//                    payment.Id.Value
//                );
//                break;

//            case "rejected":
//                await _pagoService.ProcesarOrdenRechazadaAsync(orden);
//                break;

//            case "cancelled":
//                await _pagoService.ProcesarOrdenCanceladaAsync(orden);
//                break;

//            case "pending":
//            case "in_process":
//            default:
//                await _pagoService.ProcesarOrdenPendienteAsync(orden);
//                break;
//        }



//    }
//}
//    }


