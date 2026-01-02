using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;


namespace ProyectoIntegrador_Web.Controllers
{
    using MercadoPago.Config;
    using MercadoPago.Client.Preference;
    using MercadoPago.Resource.Preference;

    public class PagoController : Controller
    {
        private readonly IConfiguration _config; // Leer AccessToken
        private readonly IOrdenRepositorio _ordenRepo;

        public PagoController(IConfiguration config, IOrdenRepositorio ordenRepo)
        {
            _config = config;
            _ordenRepo = ordenRepo;

            MercadoPagoConfig.AccessToken = _config["MercadoPago:AccessToken"];
        }

        [HttpGet]
        public async Task<IActionResult> CrearPago(Guid ordenId)
        {

            var token = _config["MercadoPago:AccessToken"];
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("AccessToken NO cargado");
            }

            //Obtener la orden
            var orden = await _ordenRepo.ObtenerOrdenPorIdAsync(ordenId);
            if (orden == null)
                return NotFound();

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

                ExternalReference = ordenId.ToString(),

                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = "https://deployprueba2025-arfrctd7agakh7eu.brazilsouth-01.azurewebsites.net/success",
                    Failure = "https://deployprueba2025-arfrctd7agakh7eu.brazilsouth-01.azurewebsites.net/failure",
                    Pending = "https://deployprueba2025-arfrctd7agakh7eu.brazilsouth-01.azurewebsites.net/pendings"
                },

                AutoReturn = "approved",

                NotificationUrl = "https://deployprueba2025-arfrctd7agakh7eu.brazilsouth-01.azurewebsites.net/api/mercadopago/webhook"
            };

            //Crear la preferencia en Mercado Pago
            var client = new PreferenceClient();
            Preference preference = await client.CreateAsync(request);
            orden.AsignarPreferenceId(preference.Id);
            await _ordenRepo.ActualizarOrdenAsync(orden);
            //Redirigir al Checkout Pro
            return Redirect(preference.InitPoint);
           
        }
    }
}
