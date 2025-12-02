using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Excepciones;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador.LogicaNegocio.ValueObjects;
using ProyectoIntegrador_Web.Models;

namespace ProyectoIntegrador_Web.Controllers
{
    public class ArtesanoController : Controller
    {
        // GET: ArtesanoController
        

        //---------------------------------------------------//
        private readonly IObtenerArtesano _obtenerArtesano;
        private readonly IEditarArtesano _editarAresano;
        private readonly IObtenerProductoArtesano _obtenerProductoArtesano;
        private readonly IWebHostEnvironment _env;

        public ArtesanoController(IWebHostEnvironment env, IObtenerArtesano obtenerArtesano, IEditarArtesano editarAresano, IObtenerProductoArtesano obtenerProductoArtesano)
        {
            _obtenerArtesano = obtenerArtesano;
            _editarAresano = editarAresano;
            _obtenerProductoArtesano = obtenerProductoArtesano;
            _env = env;
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

            var artesano = _obtenerArtesano.Ejecutar(email);

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
        public IActionResult PerfilArtesano(EditarArtesanoViewModel modelo, IFormFile archivoFoto)
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            ModelState.Remove("archivoFoto");
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "No se pudo actualizar el perfil.";
                return View(modelo);
            }

            // Obtener artesano
            var artesano = _obtenerArtesano.Ejecutar(email);
            if (artesano == null)
            {
                return NotFound("No se encontró el artesano para actualizar.");
            }

            try
            {
                // =========================
                //   SI SUBIÓ FOTO NUEVA
                // =========================
                if (archivoFoto != null && archivoFoto.Length > 0)
                {
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

                    // Guardado físico
                    var nombreArchivo = Guid.NewGuid() + extension; //genera un identificador único
                    var uploads = Path.Combine(_env.WebRootPath, "images/usuarios"); //Esto construye la ruta completa donde se guardará el archivo ej: C:\proyecto\wwwroot\images\usuarios

                    var filePath = Path.Combine(uploads, nombreArchivo); //Construye la ruta completa del archivo ej: C:\proyecto\wwwroot\images\usuarios\a8f2e9c1-4135-4e37-9b35-51f12e77a0fb.jpg
                    using (var stream = new FileStream(filePath, FileMode.Create))
                        archivoFoto.CopyTo(stream);

                    artesano.foto = "/images/usuarios/" + nombreArchivo; 


                    // ======================
                    //  ACTUALIZAR CAMPOS
                    // ======================
                }
                artesano.descripcion = modelo.Descripcion;
                artesano.telefono = modelo.Telefono;
                artesano.nombre = modelo.Nombre;
                artesano.apellido = modelo.Apellido;
                artesano.password = modelo.Password;

                // Validaciones de dominio
                artesano.Validar();
                artesano.ValidarTelefono(modelo.Telefono);

                // Guardar cambios
                _editarAresano.Actualizar(artesano);
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

        public IActionResult ProductosDelArtesano()
        {
            // Obtener el email del usuario logueado
            var email = HttpContext.Session.GetString("loginUsuario");

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Login");
            }

            // Obtener al artesano por email
            var artesano = _obtenerProductoArtesano.obtenerProductos(email);
            if (artesano == null)
            {
                return RedirectToAction("Login", "Login");
            }

            // Obtener los productos del artesano
            var productos =  artesano.productos;

            // Crear el modelo para la vista
            var modelo = new ProductosDelArtesanoViewModel
            {
                Productos = productos
            };

            return View(modelo);
        }

        // GET: ArtesanoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtesanoController/Create
        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
