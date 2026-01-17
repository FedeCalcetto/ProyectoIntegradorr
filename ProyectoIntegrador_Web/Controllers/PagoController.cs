using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;


namespace ProyectoIntegrador_Web.Controllers
{
    using MercadoPago.Client.Preference;
    using MercadoPago.Config;
    using MercadoPago.Resource.Preference;
    using ProyectoIntegrador.LogicaAplication.Interface;
    using ProyectoIntegrador.LogicaNegocio.Entidades;
    using ProyectoIntegrador_Web.Services;

    public class PagoController : Controller
    {
        private readonly IConfiguration _config; // Leer AccessToken
        private readonly IOrdenRepositorio _ordenRepo;
        private readonly IObtenerUsuario _obtenerUsuario;
        private readonly ICarritoService _carritoService;
        public PagoController(IConfiguration config, IOrdenRepositorio ordenRepo, IObtenerUsuario obtenerUsuario, ICarritoService carritoService)
        {
            _config = config;
            _ordenRepo = ordenRepo;
            _obtenerUsuario = obtenerUsuario;
            _carritoService = carritoService;

            MercadoPagoConfig.AccessToken = _config["MercadoPago:AccessToken"];
        }

        [HttpGet]
        public async Task<IActionResult> CrearPago(Guid ordenId)
        {

           
            //Obtener la orden
            var orden = await _ordenRepo.ObtenerOrdenPorIdAsync(ordenId);
            if (orden == null)
                return NotFound();

            //var artesano = orden.Artesano;

            //if (string.IsNullOrEmpty(artesano.MercadoPagoAccessToken))
            //    throw new Exception("El artesano no tiene Mercado Pago conectado");

            //MercadoPagoConfig.AccessToken = artesano.MercadoPagoAccessToken;

            //Crear la preference 
            var request = new PreferenceRequest
            {
                Items = orden.Items.Select(i => new PreferenceItemRequest
                {
                    Title = i.NombreProducto,
                    Quantity = i.Cantidad,
                    CurrencyId = "UYU",
                    UnitPrice = i.PrecioUnitario
                }).ToList(),


                PaymentMethods = new PreferencePaymentMethodsRequest
                {
                    ExcludedPaymentTypes = new List<PreferencePaymentTypeRequest>
        {
                new PreferencePaymentTypeRequest
                {
                    Id = "ticket" // Para excluir pago en efectivo
                }
        }
                },

                ExternalReference = ordenId.ToString(),

                BackUrls = new PreferenceBackUrlsRequest
                {
                    //Success = "https://localhost:7131/Pago/Success",
                    //Failure = "https://localhost:7131/Carrito",
                    //Pending = "https://localhost:7131/Carrito"
                    Success = "https://deployprueba2025-arfrctd7agakh7eu.brazilsouth-01.azurewebsites.net/pago/success",
                    Failure = "https://deployprueba2025-arfrctd7agakh7eu.brazilsouth-01.azurewebsites.net/pago/failure",
                    Pending = "https://deployprueba2025-arfrctd7agakh7eu.brazilsouth-01.azurewebsites.net/pago/pending"
                },
                AutoReturn = "approved",


                NotificationUrl = "https://deployprueba2025-arfrctd7agakh7eu.brazilsouth-01.azurewebsites.net/api/mercadopago/webhook"
            };

            //Crear la preferencia en Mercado Pago
            var client = new PreferenceClient();
            Preference preference = await client.CreateAsync(request);
            orden.AsignarPreferenceId(preference.Id);
            ViewBag.preferenceID = preference.Id;
            await _ordenRepo.ActualizarOrdenAsync(orden);
            //Redirigir al Checkout Pro
            return Redirect(preference.InitPoint);
           
        }

        [HttpGet]
        public async Task<IActionResult> Success(
            string status,
            string external_reference)
        {
            if (!Guid.TryParse(external_reference, out var ordenId))
                return BadRequest();

            var orden = await _ordenRepo.ObtenerOrdenPorIdAsync(ordenId);

            if (orden == null)
                return NotFound();

            if (orden.Estado == EstadoOrden.Pagada)
            {
                // Limpiar carrito
                var email = HttpContext.Session.GetString("loginUsuario");
                var usuario = _obtenerUsuario.Ejecutar(email);

                await _carritoService.LimpiarCarritoAsync(usuario.id);
            }

            return RedirectToAction("FacturaPorOrden","FacturaNoFiscal",new {ordenId });
        }
        [HttpGet]
        public IActionResult Pending(string external_reference)
        {
            return View("PagoPendiente");
        }
        [HttpGet]
        public IActionResult Failure(string external_reference)
        {
            return View("PagoFallido");
        }


    }
}
