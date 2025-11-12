using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador_Web.Models;

namespace ProyectoIntegrador_Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IAgregarUsuario _agregarUsuario;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, IAgregarUsuario agregarUsuario)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _agregarUsuario = agregarUsuario;
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
            HttpContext.Session.SetString("loginUsuario", usuario.email.email);
            HttpContext.Session.SetString("Rol", usuario.rol);
            if (usuario is Cliente)
            {
                return RedirectToAction("Inicio", "Cliente");

            }
            else if (usuario is Artesano)
            {
                return RedirectToAction("Inicio", "Artesano");
            }
            else
            {
                return RedirectToAction("Inicio", "Admin");
            }

        }

        public IActionResult registroUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult registroUsuario(RegistroUsuarioViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            if (modelo.soyArtesano == true)
            {
                var entidad = new Artesano
                {
                    nombre = modelo.nombre,
                    apellido = modelo.apellido,
                    email = modelo.email,
                    password = modelo.password,
                    //Rol = "Usuario"
                };
                try
                {
                    _agregarUsuario.Ejecutar(entidad);
                    return RedirectToAction("Inicio", "Artesano");
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
            else
            {
                var entidad = new Cliente
                {
                    nombre = modelo.nombre,
                    apellido = modelo.apellido,
                    email = modelo.email,
                    password = modelo.password,
                    //Rol = "Usuario"
                };
                try
                {
                    _agregarUsuario.Ejecutar(entidad);
                    return RedirectToAction("Inicio", "Cliente");
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(modelo);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(modelo);
                }
            }


            // return RedirectToAction("Index", "Home");
        }


        // GET: LoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginController/Create
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

        // GET: LoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginController/Edit/5
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

        // GET: LoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController/Delete/5
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
    }
}
