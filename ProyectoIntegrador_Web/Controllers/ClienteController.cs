using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Excepciones;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador.LogicaNegocio.ValueObjects;
using ProyectoIntegrador_Web.Models;
using ProyectoIntegrador_Web.Services;

namespace ProyectoIntegrador_Web.Controllers
{
    public class ClienteController : Controller
    {

        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IObtenerCliente _obtenerCliente;
        private readonly EmailService _email; //esto se agrega siempre que se precise enviar un email

        public ClienteController(IClienteRepositorio clienteRepositorio,
                          IObtenerCliente obtenerCliente,
                          EmailService email)
        {
            _clienteRepositorio = clienteRepositorio;
            _obtenerCliente = obtenerCliente;
            _email = email;
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
        public IActionResult Perfil(EditarClienteViewModel modelo)
        {
            try
            {
                var email = HttpContext.Session.GetString("loginUsuario");
                modelo.DepartamentosOpciones = ObtenerDepartamentos();
                if (!ModelState.IsValid)
                {
                    modelo.DepartamentosOpciones = ObtenerDepartamentos();

                    return View(modelo);
                }

                // Buscar el cliente por email (value object reconstruido)
                var cliente = _obtenerCliente.Ejecutar(email);
                if (cliente == null)
                {
                    return NotFound("No se encontró el cliente para actualizar.");
                }
                cliente.Validar();

                // Actualizar propiedades del dominio
                cliente.nombre = modelo.Nombre;
                cliente.apellido = modelo.Apellido;
                //cliente.email = new Email(modelo.Email); 

                cliente.direccion = new Direccion
                {
                    domicilio = modelo.Domicilio,
                    departamento = modelo.Departamento,
                    barrio = modelo.Barrio
                };
                cliente.foto = modelo.Foto ?? cliente.foto;

                // Guardar cambios
                _clienteRepositorio.Actualizar(cliente);
                modelo.DepartamentosOpciones = ObtenerDepartamentos();
                // Mensaje temporal para la vista
                TempData["Mensaje"] = "Perfil actualizado correctamente.";

                // Redirige nuevamente al perfil (GET)
                return RedirectToAction("Perfil");
            }
            catch (validarNombreException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(modelo);
            }
        }
        public IActionResult CambioContra()
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
            return View(new CambioPassowrdCliente());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CambioContra(CambioPassowrdCliente modelo)
        {
            var email = HttpContext.Session.GetString("loginUsuario");

            if (!ModelState.IsValid)
                return View(modelo);

            var cliente = _obtenerCliente.Ejecutar(email);
            if (cliente == null)
                return NotFound();

            try
            {
               // cliente.validarContra(modelo.Password, modelo.PasswordRepetida);
                _clienteRepositorio.cambioContra(cliente, modelo.Password, modelo.PasswordRepetida,modelo.passwordActual);
                TempData["Mensaje"] = "Contraseña cambiada exitosamente.";
                return RedirectToAction("CambioContra");
            }
            catch (Exception  ex)
            {
                ModelState.AddModelError("", ex.Message);
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

        [HttpPost]
        public IActionResult CambiarFotoCliente(IFormFile archivoFotoCliente)
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            var cliente = _clienteRepositorio.obtenerCliente(email);

            if (archivoFotoCliente == null || archivoFotoCliente.Length == 0)
            {
                TempData["Error"] = "Debes seleccionar un archivo.";
                return RedirectToAction("Perfil");
            }
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
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/usuarios");

            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            var filePath = Path.Combine(uploads, nombreArchivo);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                archivoFotoCliente.CopyTo(stream);
            }

            cliente.foto = "/images/usuarios/" + nombreArchivo;
            _clienteRepositorio.Actualizar(cliente);

            TempData["Mensaje"] = "¡Foto actualizada con éxito!";
            return RedirectToAction("Perfil");
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


        public IActionResult CuentaEliminada()
        {
            return View();
        }


        [HttpPost]
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

            // eliminar usuario
            var cliente = _obtenerCliente.Ejecutar(email);
            _clienteRepositorio.Eliminar(cliente.id);

            // limpiar sesión
            HttpContext.Session.Clear();

            return RedirectToAction("CuentaEliminada");
        }


    }
}
