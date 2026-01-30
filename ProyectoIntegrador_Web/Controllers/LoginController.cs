using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoIntegrador.LogicaAplication.Dtos;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador_Web.Models;
using ProyectoIntegrador_Web.Services;
using System.Net.Http.Json;

namespace ProyectoIntegrador_Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IAgregarUsuario _agregarUsuario;
        private readonly ILogin _loginCu;
        private readonly EmailService _email;
        private readonly IConfiguration _config;

        public LoginController(
            IUsuarioRepositorio usuarioRepositorio,
            IAgregarUsuario agregarUsuario,
            EmailService email,
            IConfiguration config, ILogin login)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _agregarUsuario = agregarUsuario;
            _email = email;
            _config = config;
            _loginCu = login;
        }

        // ===========================================================
        // 🟦 VALIDACIÓN reCAPTCHA
        // ===========================================================
        private async Task<bool> ValidarReCaptcha()
        {
            var secretKey = _config["GoogleReCaptcha:SecretKey"];
            var captchaResponse = Request.Form["g-recaptcha-response"];

            if (string.IsNullOrEmpty(captchaResponse))
                return false;

            using var client = new HttpClient();
            var result = await client.PostAsync(
                $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={captchaResponse}",
                null
            );

            var json = await result.Content.ReadFromJsonAsync<ReCaptchaResponse>();
            return json.success;
        }

        public class ReCaptchaResponse
        {
            public bool success { get; set; }
        }

        // ===========================================================
        // LOGIN GET
        // ===========================================================
        public ActionResult Login()
        {
            return View();
        }

        // ===========================================================
        // LOGIN POST
        // ===========================================================
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel modelo)
        {
            if (!ModelState.IsValid)
                return View(modelo);

            // 🟦 Validar reCAPTCHA
            if (!await ValidarReCaptcha())
            {
                ModelState.AddModelError("", "Debes completar el reCAPTCHA.");
                return View(modelo);
            }

            Usuario usuario = _loginCu.Ejecutar(modelo.Email, modelo.Password);

            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos");
                return View(modelo);
            }

            // 🛑 usuario sin verificar
            if (!usuario.Verificado)
                return RedirectToAction("VerificarEmail", new { email = usuario.email.email });

            // Login OK
            HttpContext.Session.SetString("loginUsuario", usuario.email.email);
            HttpContext.Session.SetString("Rol", usuario.rol);

            if (usuario is Cliente)
                return RedirectToAction("Catalogo", "Usuario");
            else if (usuario is Artesano)
                return RedirectToAction("FacturasArtesano", "FacturaNoFiscal");
            else
                return RedirectToAction("Inicio", "Admin");
        }

        // ===========================================================
        // REGISTRO GET
        // ===========================================================
        public IActionResult registroUsuario()
        {
            return View();
        }

        // ===========================================================
        // REGISTRO POST
        // ===========================================================
        [HttpPost]
        public async Task<IActionResult> registroUsuario(RegistroUsuarioViewModel modelo)
        {
            if (!ModelState.IsValid)
                return View(modelo);

            ///////////////////captcha///////////////////
            var captchaOK = await ValidarReCaptcha();
            if (!captchaOK)
            {
                ModelState.AddModelError("", "Debes completar el reCAPTCHA.");
                return View(modelo);
            }
            /////////////////////////////////////////////

            string codigo = new Random().Next(100000, 999999).ToString();
            string token = Guid.NewGuid().ToString("N");

            var dto = new AgregarUsuarioDto
            {
                Nombre = modelo.nombre,
                Apellido = modelo.apellido,
                Email = modelo.email,
                Password = modelo.password,
                EsArtesano = modelo.soyArtesano
            };

            try
            {
                var entidad = _agregarUsuario.Ejecutar(dto, codigo);

                entidad.CodigoVerificacion = codigo;
                entidad.TokenVerificacionEmail = token;
                entidad.TokenVerificacionEmailExpira = DateTime.UtcNow.AddMinutes(30);

                _usuarioRepositorio.Actualizar(entidad);

                await _email.EnviarCodigoAsync(entidad.email.email, codigo, "verificacion", token);

                // Guardar expiración del código
                HttpContext.Session.SetString("Codigo_" + entidad.email.email, codigo);
                HttpContext.Session.SetString(
                    "CodigoExpira_" + entidad.email.email,
                    DateTime.Now.AddMinutes(10).ToString()
                );

                return RedirectToAction("VerificarEmail", new { email = entidad.email.email });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(modelo);
            }
        }

        // ===========================================================
        // VERIFICAR EMAIL GET
        // ===========================================================
        public IActionResult VerificarEmail(string email, string token = null)
        {
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login");
            }

            var usuario = _usuarioRepositorio.BuscarPorEmail(email);

            if (usuario == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(token)
                && usuario.TokenVerificacionEmail == token
                && usuario.TokenVerificacionEmailExpira > DateTime.UtcNow)
            {
                // Validación automática exitosa
                usuario.Verificado = true;
                _usuarioRepositorio.Actualizar(usuario);

                // Podrías usar TempData para mostrar mensaje en login
                TempData["MensajeVerificacionEmail"] = "Correo verificado correctamente."; //uso otro tempdata porque 
                //al entrar al perfil aparecia ese mensaje ya que tambien usa tempdata al actualizar el perfil.

                // Redirigir al login u otra vista de éxito
                return View("VerificadoCorrectamente");
            }

            // Si no viene token o no es válido, mostrar la vista para ingresar código manual
            ViewBag.Email = email;
            return View();
        }

        // ===========================================================
        // VERIFICAR EMAIL POST
        // ===========================================================
        [HttpPost, ActionName("VerificarEmail")]
        public IActionResult VerificarEmailPost(string email, string codigo)
        {
            var usuario = _usuarioRepositorio.BuscarPorEmail(email);
            if (usuario == null)
                return NotFound();

            var expiraStr = HttpContext.Session.GetString("CodigoExpira_" + email);
            var codigoGuardado = HttpContext.Session.GetString("Codigo_" + email);

            if (expiraStr == null || codigoGuardado == null)
            {
                ViewBag.Error = "El código expiró. Solicita uno nuevo.";
                ViewBag.Email = email;
                return View();
            }

            DateTime expira = DateTime.Parse(expiraStr);

            if (DateTime.Now > expira)
            {
                ViewBag.Error = "El código expiró. Solicita uno nuevo.";
                ViewBag.Email = email;
                return View();
            }

            if (codigoGuardado != codigo)
            {
                ViewBag.Error = "Código incorrecto.";
                ViewBag.Email = email;
                return View();
            }

            usuario.Verificado = true;
            usuario.CodigoVerificacion = null;
            _usuarioRepositorio.Actualizar(usuario);

            HttpContext.Session.Remove("Codigo_" + email);
            HttpContext.Session.Remove("CodigoExpira_" + email);

            return View("VerificadoCorrectamente");
        }

        // ===========================================================
        // REENVIAR CÓDIGO
        // ===========================================================
        [HttpPost]
        public async Task<IActionResult> ReenviarCodigo(string email)
        {
            var usuario = _usuarioRepositorio.BuscarPorEmail(email);

            if (usuario == null)
                return NotFound();

            string nuevoCodigo = new Random().Next(100000, 999999).ToString();
            string nuevoToken = Guid.NewGuid().ToString("N");

            usuario.CodigoVerificacion = nuevoCodigo;
            usuario.TokenVerificacionEmail = nuevoToken;
            usuario.TokenVerificacionEmailExpira = DateTime.UtcNow.AddMinutes(30);

            _usuarioRepositorio.Actualizar(usuario);

            await _email.EnviarCodigoAsync(
                email,
                nuevoCodigo,
                "verificacion",
                nuevoToken
            );

            ViewBag.Email = email;
            ViewBag.Mensaje = "Se envió un nuevo código a tu correo.";

            return View("VerificarEmail");
        }

    }
}