using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoIntegrador.LogicaAplication.Dtos;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador_Web.Models;

namespace ProyectoIntegrador_Web.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IObtenerArtesano _obtenerArtesano;
        private readonly IAgregarProducto _agregarProducto;
        private readonly IObtenerCategorias _obtenerCategorias;
        private readonly IObtenerSubcategorias _subCategoria;
        private readonly IWebHostEnvironment _env;

        public ProductoController(IWebHostEnvironment env, IObtenerCategorias obtenerCategorias, ISubCategoriaRepositorio subCategoria, IObtenerArtesano obtenerArtesano, IAgregarProducto producto, IObtenerSubcategorias obtenerSubcategorias)
        {
            _obtenerArtesano = obtenerArtesano;
            _agregarProducto = producto;
            _obtenerCategorias = obtenerCategorias;
            _subCategoria = obtenerSubcategorias;
            _env = env;
        }
        public IActionResult AltaProducto()
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            var rol = HttpContext.Session.GetString("Rol")?.Trim().ToUpper();

            if (string.IsNullOrEmpty(email) || rol != "ARTESANO")
            {
                return RedirectToAction("Login", "Login");
            }

            var artesano = _obtenerArtesano.Ejecutar(email);

            if (artesano == null)
            {
                return NotFound();
            }
            var model = new AltaProductoViewModel();
            model.Categorias = _obtenerCategorias.ObtenerTodos(); // Cargar categorías
            return View(model);
        }

        public ActionResult GetSubcategorias(int categoriaId)
        {
            var subcategorias = _subCategoria.obtenerTodas()
        .Where(s => s.categoriaId == categoriaId)
        .Select(s => new {
            id = s.Id,
            nombre = s.Nombre
        });

            return Json(subcategorias);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AltaProducto(AltaProductoViewModel modelo)
        {
            var email = HttpContext.Session.GetString("loginUsuario");

            if (!ModelState.IsValid)
            {
                modelo.Categorias = _obtenerCategorias.ObtenerTodos();
                return View(modelo);
            }

            var artesano = _obtenerArtesano.Ejecutar(email);
            if (artesano == null)
                return NotFound();

            // Guardar imagen si se subió
            string nombreArchivo = null;

            if (modelo.ArchivoImagen != null && modelo.ArchivoImagen.Length > 0)
            {


                var carpeta = Path.Combine(_env.WebRootPath, "images/productos");

                nombreArchivo = Guid.NewGuid() + Path.GetExtension(modelo.ArchivoImagen.FileName);
                var ruta = Path.Combine(carpeta, nombreArchivo);

                using (var stream = new FileStream(ruta, FileMode.Create))
                {
                    modelo.ArchivoImagen.CopyTo(stream);
                }
            }

            try

            {
                var dto = new AgregarProductoDto
                {
                    Nombre = modelo.nombre,
                    Descripcion = modelo.descripcion,
                    Precio = modelo.precio,
                    Stock = modelo.stock,
                    SubCategoriaId = modelo.SubCategoriaId,
                    Imagen = nombreArchivo
                };

                _agregarProducto.Ejecutar(dto, artesano);
                TempData["Mensaje"] = "Producto agregado correctamente.";
                return RedirectToAction("AltaProducto");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                modelo.Categorias = _obtenerCategorias.ObtenerTodos();
                return View(modelo);
            }
        }


        // GET: ProductoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductoController/Create
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

        // GET: ProductoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductoController/Edit/5
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

        // GET: ProductoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductoController/Delete/5
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
