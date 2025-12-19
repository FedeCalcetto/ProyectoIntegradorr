using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using ProyectoIntegrador.LogicaAplication.Dtos;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Excepciones;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador_Web.Models;
using ProyectoIntegrador_Web.Services;

namespace ProyectoIntegrador_Web.Controllers
{
    public class UsuarioController : Controller
    {


        private readonly ICambiarPassword _cambiarPassword;
        private readonly IEliminarUsuario _eliminarUsuario;
        private readonly EmailService _email;
        private readonly IBusquedaDeUsuarios _busquedaDeUsuarios;
        private readonly ICatalogoService _catalogoService;
        private readonly IBusquedaDeUsuarios _busquedaDeUsuarios;

        public UsuarioController(ICambiarPassword cambiarPassword,IEliminarUsuario eliminarUsuario,EmailService email, ICatalogoService catalogoService,IBusquedaDeUsuarios busquedaDeUsuarios)
        {
            _cambiarPassword = cambiarPassword;
            _eliminarUsuario = eliminarUsuario;
            _email = email;
            _catalogoService = catalogoService;
            _busquedaDeUsuarios = busquedaDeUsuarios;
        }

        
            public IActionResult CambioContra(string returnUrl)
            {
            ViewBag.ReturnUrl = returnUrl ?? "/"; 
            return View();
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CambioContra(CambioPassowrdViewModel modelo, string returnUrl)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(modelo);
            }
            var email = HttpContext.Session.GetString("loginUsuario");

            if (email == null)
                return RedirectToAction("Login", "Login");

            try
            {
                _cambiarPassword.Ejecutar(modelo.Password, modelo.PasswordRepetida, modelo.passwordActual, email);

                TempData["Mensaje"] = "Contraseña actualizada correctamente.";

                return Redirect(returnUrl ?? "/");
            }
            catch (MayusculaPasswordException ex)
            {
                ModelState.AddModelError("", "La contraseña debe contener mayúsucla");
                ViewBag.ReturnUrl = returnUrl;
                return View(modelo);
            }
            catch (ContraActualException ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.ReturnUrl = returnUrl;
                return View(modelo);
            }
            catch (NoCoincideException ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.ReturnUrl = returnUrl;
                return View(modelo);
            }
            catch (SonIgualesException ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.ReturnUrl = returnUrl;
                return View(modelo);
            }
            catch (numeroPassowordException ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.ReturnUrl = returnUrl;
                return View(modelo);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error inesperado.");
                ViewBag.ReturnUrl = returnUrl;
                return View(modelo);
            }
          
            
        }

        public IActionResult BusquedaDeUsuarios(string filtro)
        {

            var email = HttpContext.Session.GetString("loginUsuario");
            var rol = HttpContext.Session.GetString("Rol")?.Trim().ToUpper();

            if (string.IsNullOrEmpty(email) || (rol != "ARTESANO" && rol != "CLIENTE"))
            {
                return RedirectToAction("Login", "Login");
            }

            var modelo = new BusquedaDeUsuariosViewModel();
            modelo.Filtro = filtro;

            if (!string.IsNullOrEmpty(filtro))
            {
                modelo.Usuarios = _busquedaDeUsuarios.Ejecutar(filtro);
            }

            return View(modelo);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult BusquedaDeUsuarios(BusquedaDeUsuariosViewModel modelo)
        //{

        //    modelo.Usuarios = _busquedaDeUsuarios.Ejecutar(modelo.Filtro);

        //    return View(modelo);
        //}

        public IActionResult PerfilPublico()
        {
            return View();
        }
        // GET: UsuarioController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // eliminacion del usuario

        public IActionResult CuentaEliminada()
        {
            return View();
        }

        public async Task<IActionResult> ConfirmarEliminacion()
        {
            var email = HttpContext.Session.GetString("loginUsuario");

            if (email == null)
                return RedirectToAction("Login", "Login");

            string codigo = new Random().Next(100000, 999999).ToString();

            HttpContext.Session.SetString("CodigoEliminar_" + email, codigo);
            HttpContext.Session.SetString("EliminarExpira_" + email,
                DateTime.Now.AddMinutes(10).ToString());

            await _email.EnviarCodigoAsync(email, codigo, "eliminacion");

            TempData["Mensaje"] = "Te enviamos un código para confirmar la eliminación de tu cuenta.";

            return View(new EliminarCuentaViewModel { Email = email });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarCuenta(EliminarCuentaViewModel modelo)
        {
            var email = modelo.Email;
            var codigoIngresado = modelo.Codigo;

            var codigoGuardado = HttpContext.Session.GetString("CodigoEliminar_" + email);
            var expiraStr = HttpContext.Session.GetString("EliminarExpira_" + email);

            if (codigoGuardado == null || expiraStr == null)
            {
                TempData["Error"] = "El código expiró. Pedí uno nuevo.";
                return RedirectToAction("ConfirmarEliminacion");
            }

            DateTime expira = DateTime.Parse(expiraStr);
            if (DateTime.Now > expira)
            {
                TempData["Error"] = "El código expiró.";
                return RedirectToAction("ConfirmarEliminacion");
            }

            if (codigoGuardado != codigoIngresado)
            {
                TempData["Error"] = "Código incorrecto.";
                return RedirectToAction("ConfirmarEliminacion");
            }

            // aca llamo al caso de uso.
            _eliminarUsuario.Ejecutar(email);

            HttpContext.Session.Clear();

            return RedirectToAction("CuentaEliminada");
        }
        //////////////////////////////////////////////////////////////////
        ////////////////////// catalogo de usuarios //////////////////////
        //////////////////////////////////////////////////////////////////
        
        public IActionResult CatalogoUsuarios()
        {
            return View();

        }

        public IActionResult Catalogo()
        {
            var vm = _catalogoService.ObtenerCatalogoInicial();
            return View(vm);
        }

    }
}
