using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Excepciones;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador.LogicaNegocio.ValueObjects;
using ProyectoIntegrador_Web.Models;

namespace ProyectoIntegrador_Web.Controllers
{
    public class ClienteController : Controller
    {

        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IObtenerCliente _obtenerCliente;
        private readonly IWebHostEnvironment _env;
        public ClienteController(IWebHostEnvironment env, IClienteRepositorio clienteRepositorio, IObtenerCliente obtenerCliente)
        {
            _clienteRepositorio = clienteRepositorio;
            _obtenerCliente = obtenerCliente;
            _env = env;
        }

        //seelct departamentos
        private List<string> ObtenerDepartamentos()
        {
            return new List<string>
            {
            "Artigas",
            "Salto",
            "Rivera",
            "Tacuarembó",
            "Río Negro",
            "Soriano",
            "Durazno",
            "Canelones",
            "Rocha",
            "Montevideo",
            "Colonia",
            "San José",
            "Treinta y Tres",
            "Florida",
            "Cerro Largo",
            "Paysandú",
            "Flores",
            "Lavalleja",
            "Maldonado"
            };
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
                Domicilio = cliente.direccion?.domicilio,
                Departamento = cliente.direccion?.departamento,
                Barrio = cliente.direccion?.barrio,
                Foto = cliente.foto,

                DepartamentosOpciones = ObtenerDepartamentos()
            };

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Perfil(EditarClienteViewModel modelo, IFormFile archivoFotoCliente)
        {
            var email = HttpContext.Session.GetString("loginUsuario");

            modelo.DepartamentosOpciones = ObtenerDepartamentos();
            ModelState.Remove("archivoFotoCliente");
            if (!ModelState.IsValid)
                return View(modelo);

            var cliente = _obtenerCliente.Ejecutar(email);
            if (cliente == null)
                return NotFound("No se encontró el cliente para actualizar.");

            try
            {
                // =============================
                //     SI SUBIÓ FOTO NUEVA
                // =============================
                if (archivoFotoCliente != null && archivoFotoCliente.Length > 0)
                {
                    var tiposPermitidos = new[] { "image/jpeg", "image/png", "image/jpg" };
                    if (!tiposPermitidos.Contains(archivoFotoCliente.ContentType))
                    {
                        TempData["Error"] = "El archivo debe ser una imagen JPG o PNG.";
                        return RedirectToAction("Perfil");
                    }

                    var extension = Path.GetExtension(archivoFotoCliente.FileName).ToLower();
                    var extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png" };

                    if (!extensionesPermitidas.Contains(extension))
                    {
                        TempData["Error"] = "Formato no permitido. Usa JPG o PNG.";
                        return RedirectToAction("Perfil");
                    }

                    var nombreArchivo = Guid.NewGuid() + extension;
                    var uploads = Path.Combine(_env.WebRootPath, "images/usuarios");

                    var filePath = Path.Combine(uploads, nombreArchivo);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                        archivoFotoCliente.CopyTo(stream);

                    cliente.foto = "/images/usuarios/" + nombreArchivo;
                }

                // =============================
                //     ACTUALIZAR CAMPOS
                // =============================
                cliente.nombre = modelo.Nombre;
                cliente.apellido = modelo.Apellido;

                cliente.direccion = new Direccion
                {
                    domicilio = modelo.Domicilio,
                    departamento = modelo.Departamento,
                    barrio = modelo.Barrio
                };

                _clienteRepositorio.Actualizar(cliente);

                TempData["Mensaje"] = "Perfil actualizado correctamente.";
                return RedirectToAction("Perfil");
            }
            catch (validarNombreException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(modelo);
            }
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
