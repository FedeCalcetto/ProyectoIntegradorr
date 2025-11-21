using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Excepciones;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador.LogicaNegocio.ValueObjects;
using ProyectoIntegrador_Web.Models;

namespace ProyectoIntegrador_Web.Controllers
{
    public class ArtesanoController : Controller
    {
        // GET: ArtesanoController
        private readonly IArtesanoRepositorio _artesanorepo;

        public ArtesanoController(IArtesanoRepositorio artesanorepo)
        {
            _artesanorepo = artesanorepo;
        }
        public ActionResult Inicio()
        {
            if (HttpContext.Session.GetString("Rol") != null && HttpContext.Session.GetString("Rol").Trim().ToUpper().Equals("ARTESANO"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult PerfilArtesano()
        {
            var email = HttpContext.Session.GetString("loginUsuario");

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Login");
            }

            var artesano = _artesanorepo.ObtenerPorEmail(email);

            if (artesano == null)
            {
                return NotFound();
            }

            var modelo = new EditarArtesanoViewModel
            {
                Descripcion = artesano.descripcion,
                Telefono = artesano.telefono,
                Nombre = artesano.nombre,
                Apellido = artesano.apellido,
                Email = artesano.email.email,
                Password = artesano.password,
                Foto = artesano.foto
            };

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PerfilArtesano(EditarArtesanoViewModel modelo)
        {
            try
            {
                var email = HttpContext.Session.GetString("loginUsuario");
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "No se puedo actualizar el perfil.";
                return View(modelo);
            }

            // Buscar el cliente por email (value object reconstruido)
            var artesano = _artesanorepo.ObtenerPorEmail(email);
            if (artesano == null)
            {
                return NotFound("No se encontró el cliente para actualizar.");
            }

            
                // Actualizar propiedades del dominio
            artesano.descripcion = modelo.Descripcion;
            artesano.telefono = modelo.Telefono;
            artesano.nombre = modelo.Nombre;
            artesano.apellido = modelo.Apellido;
            artesano.password = modelo.Password;
            artesano.foto = modelo.Foto ?? artesano.foto;

                artesano.Validar();
                artesano.ValidarTelefono(modelo.Telefono);
                
                _artesanorepo.Actualizar(artesano);

                TempData["Mensaje"] = "Perfil actualizado correctamente.";
                return RedirectToAction("PerfilArtesano");
            }
            catch (TelefonoUsuarioException ex)
            {
                ModelState.AddModelError("Telefono", ex.Message);
                return View(modelo);
            }
            catch (validarNombreException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(modelo);
            }
           
        }
        


        // GET: ArtesanoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ArtesanoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtesanoController/Create
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

        // GET: ArtesanoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ArtesanoController/Edit/5
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

        // GET: ArtesanoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ArtesanoController/Delete/5
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

        public IActionResult NuevaPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NuevaPassword(NuevaPasswordViewModel modelo)
        {
            try
            {
                if (!ModelState.IsValid)
                return View(modelo);

            var email = HttpContext.Session.GetString("loginUsuario");
            var artesano = _artesanorepo.ObtenerPorEmail(email);

            if (artesano == null)
                return NotFound();

            
                artesano.password = modelo.Password;
                artesano.validarPasswordMaysucula();
                artesano.validarNumero(); 
                artesano.validarContraseñaLongitud();
                _artesanorepo.Actualizar(artesano);

                TempData["Mensaje"] = "Contraseña actualizada correctamente.";
                return RedirectToAction("PerfilArtesano");
            }
            catch (MayusculaPasswordException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(modelo);
            }
            catch (numeroPassowordException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(modelo);
            }
            catch (passwordUsuarioException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(modelo);
            }
        }

        [HttpPost]
        public IActionResult CambiarFoto(IFormFile archivoFoto)
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            var artesano = _artesanorepo.ObtenerPorEmail(email);

            if (archivoFoto == null || archivoFoto.Length == 0)
            {
                TempData["Error"] = "Debes seleccionar un archivo.";
                return RedirectToAction("PerfilArtesano");
            }
            var tiposPermitidos = new[] { "image/jpeg", "image/png", "image/jpg" };
            if (!tiposPermitidos.Contains(archivoFoto.ContentType))
            {
                TempData["Error"] = "El archivo debe ser una imagen JPG o PNG.";
                return RedirectToAction("PerfilArtesano");
            }

            var extension = Path.GetExtension(archivoFoto.FileName).ToLower();
            var extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png" };

            if (!extensionesPermitidas.Contains(extension))
            {
                TempData["Error"] = "Formato no permitido. Usa JPG o PNG.";
                return RedirectToAction("PerfilArtesano");
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
                archivoFoto.CopyTo(stream);
            }

            artesano.foto = "/images/usuarios/" + nombreArchivo;
            _artesanorepo.Actualizar(artesano);

            TempData["Mensaje"] = "¡Foto actualizada con éxito!";
            return RedirectToAction("PerfilArtesano");
        }
    }
}
