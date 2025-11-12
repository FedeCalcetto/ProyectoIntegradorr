using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador.LogicaNegocio.ValueObjects;
using ProyectoIntegrador_Web.Models;

namespace ProyectoIntegrador_Web.Controllers
{
    public class ClienteController : Controller
    {

        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IObtenerCliente _obtenerCliente;

        public ClienteController(IClienteRepositorio clienteRepositorio, IObtenerCliente obtenerCliente)
        {
            _clienteRepositorio = clienteRepositorio;
            _obtenerCliente = obtenerCliente;
        }

        // GET: ClienteController
        public ActionResult Inicio()
        {
            if (HttpContext.Session.GetString("Rol") != null && HttpContext.Session.GetString("Rol").Trim().ToUpper().Equals("CLIENTE"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public IActionResult Perfil()
        {
            var email = HttpContext.Session.GetString("loginUsuario");

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Login");
            }

            var cliente = _obtenerCliente.Ejecutar(email);

            if (cliente == null)
            {
                return NotFound();
            }

            var modelo = new EditarClienteViewModel
            {
                Nombre = cliente.nombre,
                Apellido = cliente.apellido,
                Email = cliente.email.email, 
                Password = cliente.password,
                Domicilio = cliente.direccion?.domicilio,
                Departamento = cliente.direccion?.departamento,
                Barrio = cliente.direccion?.barrio
            };

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Perfil(EditarClienteViewModel modelo)
        {

            var email = HttpContext.Session.GetString("loginUsuario");
            if (!ModelState.IsValid)
            {
                // Si hay errores de validación, vuelve a mostrar la vista
                return View(modelo);
            }

            // Buscar el cliente por email (value object reconstruido)
            var cliente = _obtenerCliente.Ejecutar(email);
            if (cliente == null)
            {
                return NotFound("No se encontró el cliente para actualizar.");
            }

            // Actualizar propiedades del dominio
            cliente.nombre = modelo.Nombre;
            cliente.apellido = modelo.Apellido;
            cliente.password = modelo.Password;
            //cliente.email = new Email(modelo.Email); 

            cliente.direccion = new Direccion
            {
                domicilio = modelo.Domicilio,
                departamento = modelo.Departamento,
                barrio = modelo.Barrio
            };

            // Guardar cambios
            _clienteRepositorio.Actualizar(cliente);

            // Mensaje temporal para la vista
            TempData["Mensaje"] = "Perfil actualizado correctamente.";

            // Redirige nuevamente al perfil (GET)
            return RedirectToAction("Perfil");
        }

        // GET: ClienteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteController/Create
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

        // GET: ClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClienteController/Edit/5
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

        // GET: ClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClienteController/Delete/5
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
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
