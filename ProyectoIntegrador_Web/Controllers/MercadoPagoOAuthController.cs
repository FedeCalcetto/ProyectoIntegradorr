//using Microsoft.AspNetCore.Mvc;
//using ProyectoIntegrador.LogicaAplication.Dtos;
//using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
//using System.Text.Json;

//namespace ProyectoIntegrador_Web.Controllers
//{
//    [Route("mercadopago")]
//    public class MercadoPagoOAuthController : Controller
//    {
//        private readonly IConfiguration _config;
//        private readonly IArtesanoRepositorio _artesanoRepo;

//        public MercadoPagoOAuthController(
//            IConfiguration config,
//            IArtesanoRepositorio artesanoRepo)
//        {
//            _config = config;
//            _artesanoRepo = artesanoRepo;
//        }

//        public IActionResult Conectar()
//        {
//            var email = HttpContext.Session.GetString("loginUsuario");
//            var artesano = _artesanoRepo.ObtenerPorEmail(email);

//            //Armar state
//            var state = $"{artesano.id}|{Guid.NewGuid()}";

//            //Armar URL OAuth
//            var clientId = _config["MercadoPago:ClientId"];
//            var redirectUri = _config["MercadoPago:RedirectUri"];

//            var url =
//                "https://auth.mercadopago.com/authorization" +
//                $"?client_id={clientId}" +
//                "&response_type=code" +
//                "&platform_id=mp" +
//                $"&redirect_uri={Uri.EscapeDataString(redirectUri)}" +
//                $"&state={state}";

//            return Redirect(url);
//        }


//        [HttpGet("oauth/callback")]
//        public async Task<IActionResult> Callback(
//            string code,
//            string state)
//        {
//            // state = id del artesano
//            var partes = state.Split('|');
//            var artesanoId = int.Parse(partes[0]);

//            var clientId = _config["MercadoPago:ClientId"];
//            var clientSecret = _config["MercadoPago:ClientSecret"];
//            var redirectUri = _config["MercadoPago:RedirectUri"];

//            using var http = new HttpClient();

//            var response = await http.PostAsync(
//                "https://api.mercadopago.com/oauth/token",
//                new FormUrlEncodedContent(new Dictionary<string, string>
//                {
//                { "grant_type", "authorization_code" },
//                { "client_id", clientId },
//                { "client_secret", clientSecret },
//                { "code", code },
//                { "redirect_uri", redirectUri }
//                }));
//            if (!response.IsSuccessStatusCode)
//            {
//                throw new Exception("Error al autorizar Mercado Pago");
//            }
//            var json = await response.Content.ReadAsStringAsync();
//            var token = JsonSerializer.Deserialize<MpOAuthTokenDto>(json);
//            if (token == null || string.IsNullOrEmpty(token.access_token))
//            {
//                throw new Exception("No se pudo obtener el Access Token de Mercado Pago");
//            }

//            var artesano = _artesanoRepo.Obtener(artesanoId);

//            artesano.MercadoPagoAccessToken = token.access_token;
//            artesano.MercadoPagoRefreshToken = token.refresh_token;
//            artesano.MercadoPagoUserId = token.user_id;

//            _artesanoRepo.Actualizar(artesano);

//            return RedirectToAction("PerfilArtesano", "Artesano");
//        }
//    }

//}
