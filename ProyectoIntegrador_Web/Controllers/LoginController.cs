using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly EmailService _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio,
                               IAgregarUsuario agregarUsuario,
                               EmailService email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _agregarUsuario = agregarUsuario;
            _email = email;
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

            Usuario usuario = _usuarioRepositorio.Login(modelo.Email, modelo.Password);

            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos");
                return View(modelo);
            }

            // ❗ Bloquear login si NO verificó su correo
            if (!usuario.Verificado)
            {
                ModelState.AddModelError(string.Empty, "Debes verificar tu correo antes de iniciar sesión.");
                return View(modelo);
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

            if (modelo.soyArtesano)
            {
                entidad = new Artesano
                {
                    nombre = modelo.nombre,
                    apellido = modelo.apellido,
                    email = modelo.email,
                    password = modelo.password,
                    rol = "Artesano",
                    CodigoVerificacion = codigo,
                    Verificado = false
                };
            }
            else
            {
                entidad = new Cliente
                {
                    nombre = modelo.nombre,
                    apellido = modelo.apellido,
                    email = modelo.email,
                    password = modelo.password,
                    rol = "Cliente",
                    CodigoVerificacion = codigo,
                    Verificado = false
                };
            }

            try
            {
                _agregarUsuario.Ejecutar(entidad); // guarda en BD  

                //  Enviar correo
                await _email.EnviarCodigoAsync(entidad.email.email, codigo);

                //  Enviar a pantalla para ingresar código
                return RedirectToAction("VerificarEmail", new { email = entidad.email.email });
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(modelo);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al registrar el usuario.");
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

            if (usuario.CodigoVerificacion == codigo)
            {
                usuario.Verificado = true;
                usuario.CodigoVerificacion = null;

                _usuarioRepositorio.Actualizar(usuario);

                return View("VerificadoCorrectamente");
            }

            ViewBag.Error = "Código incorrecto";
            ViewBag.Email = email;
            return View();
        }


    }
}
