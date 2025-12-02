using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoIntegrador.LogicaAplication.Dtos;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador_Web.Models;
using ProyectoIntegrador_Web.Services;  // ← agregar esto

namespace ProyectoIntegrador_Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IAgregarUsuario _agregarUsuario;
        private readonly ILogin _loginCu;
        private readonly EmailService _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio,
                               IAgregarUsuario agregarUsuario,
                               EmailService email, ILogin login)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _agregarUsuario = agregarUsuario;
            _email = email;
            _loginCu = login;
        }

        // GET: LoginController
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            Usuario usuario = _loginCu.Ejecutar(modelo.Email, modelo.Password);

            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos");
                return View(modelo);
            }

            // ❗ Bloquear login si NO verificó su correo
            if (!usuario.Verificado)
            {
                /* ModelState.AddModelError(string.Empty, "Debes verificar tu correo antes de iniciar sesión.");
                 return View(modelo);*/

                return RedirectToAction("VerificarEmail", new { email = usuario.email.email });
            }

            HttpContext.Session.SetString("loginUsuario", usuario.email.email);
            HttpContext.Session.SetString("Rol", usuario.rol);

            if (usuario is Cliente)
                return RedirectToAction("Inicio", "Cliente");
            else if (usuario is Artesano)
                return RedirectToAction("Inicio", "Artesano");
            else
                return RedirectToAction("Inicio", "Admin");
        }

        public IActionResult registroUsuario()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> registroUsuario(RegistroUsuarioViewModel modelo)
        {
            if (!ModelState.IsValid)
                return View(modelo);

            // 🔐 Generar código
            string codigo = new Random().Next(100000, 999999).ToString();

            Usuario entidad;

            var dto = new AgregarUsuarioDto
            {
                Nombre = modelo.nombre,
                Apellido = modelo.apellido,
                Email = modelo.email.email,
                Password = modelo.password,
                EsArtesano = modelo.soyArtesano
            };

            //if (modelo.soyArtesano)
            //{
            //    entidad = new Artesano
            //    {
            //        nombre = modelo.nombre,
            //        apellido = modelo.apellido,
            //        email = modelo.email,
            //        password = modelo.password,
            //        rol = "Artesano",
            //        CodigoVerificacion = codigo,
            //        Verificado = false
            //    };
            //}
            //else
            //{
            //    entidad = new Cliente
            //    {
            //        nombre = modelo.nombre,
            //        apellido = modelo.apellido,
            //        email = modelo.email,
            //        password = modelo.password,
            //        rol = "Cliente",
            //        CodigoVerificacion = codigo,
            //        Verificado = false
            //    };
            //}

            try
            {
                _agregarUsuario.Ejecutar(dto, codigo); // guarda en BD  

                //  Enviar correo
                await _email.EnviarCodigoAsync(entidad.email.email, codigo);

                // Esto hace que el codigo dure 10 minutos en la session
                HttpContext.Session.SetString("Codigo_" + entidad.email.email, codigo);
                HttpContext.Session.SetString("CodigoExpira_" + entidad.email.email,DateTime.Now.AddMinutes(1).ToString());

                //  Enviar a pantalla para ingresar código
                return RedirectToAction("VerificarEmail", new { email = entidad.email.email });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(modelo);
            }
        }

        // GET – pantalla para ingresar código
        public IActionResult VerificarEmail(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        // POST – verificar código
        [HttpPost]
        public IActionResult VerificarEmail(string email, string codigo)
        {
            var usuario = _usuarioRepositorio.BuscarPorEmail(email);
            if (usuario == null)
                return NotFound();

            // Obtener datos de Session
            var expiraStr = HttpContext.Session.GetString("CodigoExpira_" + email);
            var codigoGuardado = HttpContext.Session.GetString("Codigo_" + email);

            if (expiraStr == null || codigoGuardado == null)
            {
                ViewBag.Error = "El código expiró. Solicita uno nuevo.";
                ViewBag.Email = email;
                return View();
            }

            // Convertir expiración
            DateTime expira = DateTime.Parse(expiraStr);

            // Usar UtcNow para evitar errores de zona horaria
            if (DateTime.Now > expira)
            {
                ViewBag.Error = "El código expiró. Solicita uno nuevo.";
                ViewBag.Email = email;
                return View();
            }

            // Comparar código ingresado vs código guardado
            if (codigoGuardado != codigo)
            {
                ViewBag.Error = "Código incorrecto.";
                ViewBag.Email = email;
                return View();
            }

            // Verificación correcta → marcar usuario
            usuario.Verificado = true;
            usuario.CodigoVerificacion = null;
            _usuarioRepositorio.Actualizar(usuario);

            // Limpiar session
            HttpContext.Session.Remove("Codigo_" + email);
            HttpContext.Session.Remove("CodigoExpira_" + email);

            return View("VerificadoCorrectamente");
        }

        //reenviar codigo verificacion
        [HttpPost]
        public async Task<IActionResult> ReenviarCodigo(string email)
        {
            var usuario = _usuarioRepositorio.BuscarPorEmail(email);

            if (usuario == null)
                return NotFound();

            // Generar nuevo código
            var nuevoCodigo = new Random().Next(100000, 999999).ToString();

            usuario.CodigoVerificacion = nuevoCodigo;
            _usuarioRepositorio.Actualizar(usuario);

            // Enviar correo
            await _email.EnviarCodigoAsync(email, nuevoCodigo);

            // 👉 Guardar código y expiración en Session
            HttpContext.Session.SetString("Codigo_" + email, nuevoCodigo);
            HttpContext.Session.SetString("CodigoExpira_" + email,
            DateTime.Now.AddMinutes(10).ToString());

            ViewBag.Email = email;
            ViewBag.Mensaje = "Se envió un nuevo código a tu correo.";

            return View("VerificarEmail");
        }
    }


}

