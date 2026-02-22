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
        private readonly IProductosFiltrados _productosFiltrados;
        private readonly IObtenerProducto _obtenerProducto;
        private readonly IObtenerTodosLosProductos _obtenerTodosLosProductos;
        private readonly IObtenerUsuario _obtenerUsuario;
        private readonly IMostrarProductosCarrito _mostrarProductosCarrito;
        private readonly IObtenerSubCategoria _obtenerSubCategoria;
        private readonly IObtenerCliente _obtenerCliente;
        private readonly IAgregarReporte _agregarReporte;
        private readonly IObtenerClienteConFavoritos _obtenerClienteConFav;


        public ProductoController(IWebHostEnvironment env, IObtenerCategorias obtenerCategorias, ISubCategoriaRepositorio subCategoria, IObtenerArtesano obtenerArtesano,
            IAgregarProducto producto, IObtenerSubcategorias obtenerSubcategorias, IObtenerTodosLosProductos obtenerTodosLosProductos, IObtenerUsuario obtenerUsuario, IMostrarProductosCarrito mostrarProductosCarrito, IProductosFiltrados productosFiltrados, IObtenerProducto obtenerProducto, IObtenerSubCategoria obtenerSubCategoria, IAgregarReporte agregarReporte, IObtenerCliente obtenerCliente, IObtenerClienteConFavoritos obtenerClienteConFav)
        private readonly ICalificarProducto _calificarProducto;
        private readonly IObtenerPromedioCalificacionDeProducto _calificacionPromedioProducto;
        public ProductoController(IWebHostEnvironment env, IObtenerCategorias obtenerCategorias, ISubCategoriaRepositorio subCategoria, IObtenerArtesano obtenerArtesano,
            IAgregarProducto producto, IObtenerSubcategorias obtenerSubcategorias, IObtenerTodosLosProductos obtenerTodosLosProductos, IObtenerUsuario obtenerUsuario, 
            IMostrarProductosCarrito mostrarProductosCarrito, IProductosFiltrados productosFiltrados, IObtenerProducto obtenerProducto, 
            IObtenerSubCategoria obtenerSubCategoria, IAgregarReporte agregarReporte, IObtenerCliente obtenerCliente, ICalificarProducto calificarProducto,
           IObtenerPromedioCalificacionDeProducto calificacionPromedioProducto)
        {
            _obtenerArtesano = obtenerArtesano;
            _agregarProducto = producto;
            _obtenerCategorias = obtenerCategorias;
            _subCategoria = obtenerSubcategorias;
            _env = env;
            _productosFiltrados = productosFiltrados;
            _obtenerProducto = obtenerProducto;
            _obtenerTodosLosProductos = obtenerTodosLosProductos;
            _obtenerUsuario = obtenerUsuario;
            _mostrarProductosCarrito = mostrarProductosCarrito;
            _obtenerSubCategoria = obtenerSubCategoria;
            _agregarReporte = agregarReporte;
            _obtenerCliente = obtenerCliente;
            _obtenerClienteConFav = obtenerClienteConFav;
            _calificarProducto = calificarProducto;
            _calificacionPromedioProducto = calificacionPromedioProducto;
        }




        public IActionResult AltaProducto()
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            var rol = HttpContext.Session.GetString("Rol")?.Trim().ToUpper();

            if (string.IsNullOrEmpty(email) || rol != "ARTESANO")
                return RedirectToAction("Login", "Login");

            var artesano = _obtenerArtesano.Ejecutar(email);
            if (artesano == null)
                return NotFound();

            var model = new AltaProductoViewModel
            {
                Categorias = _obtenerCategorias.ObtenerTodos()
            };

            return View(model);
        }


        public ActionResult GetSubcategorias(int categoriaId)
        {
            var subcategorias = _subCategoria.obtenerTodas()
                .Where(s => s.categoriaId == categoriaId)
                .Select(s => new
                {
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
                modelo.SubCategorias = _subCategoria.obtenerTodas();
                return View(modelo);
            }

            var artesano = _obtenerArtesano.Ejecutar(email);
            if (artesano == null)
                return NotFound();

            // -----------------------------
            //  IMAGEN PRINCIPAL
            // -----------------------------
            string nombreArchivo = null;

            if (modelo.ArchivoImagen != null && modelo.ArchivoImagen.Length > 0)
            {
                var carpeta = Path.Combine(_env.WebRootPath, "images/productos");

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                nombreArchivo = Guid.NewGuid() + Path.GetExtension(modelo.ArchivoImagen.FileName);
                var ruta = Path.Combine(carpeta, nombreArchivo);

                using var stream = new FileStream(ruta, FileMode.Create);
                modelo.ArchivoImagen.CopyTo(stream);
            }

            // -----------------------------
            //   MÚLTIPLES FOTOS
            // -----------------------------
            List<string> fotosExtras = new List<string>();
            //Para que la primer foto esté en el listado y me permita intercalarla con las demás en la vista
            if (nombreArchivo != null)
                fotosExtras.Add(nombreArchivo);


            if (modelo.Fotos != null && modelo.Fotos.Any())
            {
                var carpeta = Path.Combine(_env.WebRootPath, "images/productos");
                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                foreach (var foto in modelo.Fotos)
                {
                    if (foto != null && foto.Length > 0)
                    {
                        var nombre = Guid.NewGuid() + Path.GetExtension(foto.FileName);
                        var ruta = Path.Combine(carpeta, nombre);

                        using var stream = new FileStream(ruta, FileMode.Create);
                        foto.CopyTo(stream);

                        fotosExtras.Add(nombre);
                    }
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
                    Imagen = nombreArchivo,
                    Fotos = fotosExtras
                };

                _agregarProducto.Ejecutar(dto, artesano);

                TempData["Mensaje"] = "Producto agregado correctamente.";
                return RedirectToAction("AltaProducto");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                modelo.Categorias = _obtenerCategorias.ObtenerTodos();
                modelo.SubCategorias = _subCategoria.obtenerTodas();
                return View(modelo);
            }
        }

        public IActionResult ProductosFiltrados(
        string? filtro,
        int? precioMin,
        int? precioMax,
        int pagina = 1,
        int? categoriaId = null,
        int? subCategoriaId = null)
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            var rol = HttpContext.Session.GetString("Rol")?.Trim().ToUpper();

            //if (string.IsNullOrEmpty(email) || rol != "CLIENTE")
            //{
            //    return RedirectToAction("Login", "Login");
            //}

            const int tamanoPagina = 10;

            int totalRegistros;

            var productos = _productosFiltrados.Ejecutar(
                filtro,
                precioMin,
                precioMax,
                pagina,
                tamanoPagina,
                out totalRegistros,
                categoriaId,
                subCategoriaId
            );

            var modelo = new ProductosFiltradosViewModel
            {
                Productos = productos,
                Filtro = filtro,
                PrecioMin = precioMin,
                PrecioMax = precioMax,
                CategoriaId = categoriaId,
                SubCategoriaId = subCategoriaId,

                PaginaActual = pagina,
                TamanoPagina = tamanoPagina,
                TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)tamanoPagina),

                // Estos dos normalmente vienen de repositorios
                Categorias = _obtenerCategorias.ObtenerTodos(),
                SubCategorias = categoriaId.HasValue
            ? _obtenerSubCategoria.obtenerPorCtegoria(categoriaId.Value)
            : Enumerable.Empty<SubCategoria>()
            };
            return View(modelo);
        }

        public IActionResult DetallesProducto(int id)
        {
            var producto = _obtenerProducto.obtener(id);

            if (producto == null)
                return NotFound();

            var rol = HttpContext.Session.GetString("Rol")?.ToUpper();
            var email = HttpContext.Session.GetString("loginUsuario");

            bool esFavorito = false;

            if (rol == "CLIENTE" && !string.IsNullOrEmpty(email))
            {
                var cliente = _obtenerClienteConFav.Ejecutar(email);
                esFavorito = cliente?.productosFavoritos?.Any(p => p.id == id) ?? false;
            }

            var cantidad = _calificacionPromedioProducto.ObtenerTodasLasCalificaciones(producto.id);
            var promedio = _calificacionPromedioProducto.ObtenerPromedioPorProducto(producto.id);
            var vm = new DetallesProductoViewModel
            {
                Id = producto.id,
                Nombre = producto.nombre,
                Descripcion = producto.descripcion,
                Precio = producto.precio,
                Stock = producto.stock,
                Imagen = producto.imagen,
                Fotos = producto.Fotos,
                Artesano = producto.artesano?.nombre,
                SubCategoria = producto.SubCategoria?.Nombre,
                ArtesanoId = producto.artesano.id,
                Reporte = new AgregarReporteDto(),

                EsFavorito = esFavorito
                PromedioCalificacion = (double)promedio,
                CantidadReseñas = cantidad
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReportarProducto(DetallesProductoViewModel modelo)
        {
            
            if (!ModelState.IsValid)
            {
                var vm = CrearDetalleVM(modelo.Id);
                vm.Reporte = modelo.Reporte;
                return View("DetallesProducto", vm);

            }
            var producto = _obtenerProducto.obtener(modelo.Id);

            if (HttpContext.Session.GetString("Rol") == null)
            {
                TempData["Error"] =
                    "Debe iniciar sesión para reportar un producto al carrito.";

                return RedirectToAction("DetallesProducto", "Producto", new { id = producto.id });
            } 
            var email = HttpContext.Session.GetString("loginUsuario");
            var cliente = _obtenerCliente.Ejecutar(email);
            try
            {
                _agregarReporte.Ejecutar(
                    modelo.Reporte,
                    artesano: null,
                    cliente: cliente,
                    producto: producto
                );

                TempData["Success"] = "Reporte enviado correctamente";
                return RedirectToAction("DetallesProducto", new { id = modelo.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                var vm = CrearDetalleVM(modelo.Id);
                vm.Reporte = modelo.Reporte;
                return View("DetallesProducto", vm);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Calificar([FromBody] CalificarDto dto)
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            var usuario = _obtenerUsuario.Ejecutar(email);
            await _calificarProducto.Ejecutar(dto.productoId, dto.puntaje, usuario.id);

            return Ok();
        }
        private DetallesProductoViewModel CrearDetalleVM(int id)
        {
            var producto = _obtenerProducto.obtener(id);

            var cantidad = _calificacionPromedioProducto.ObtenerTodasLasCalificaciones(producto.id);
            var promedio = _calificacionPromedioProducto.ObtenerPromedioPorProducto(producto.id);
            return new DetallesProductoViewModel
            {
                Id = producto.id,
                Nombre = producto.nombre,
                Descripcion = producto.descripcion,
                Precio = producto.precio,
                Stock = producto.stock,
                Imagen = producto.imagen,
                Fotos = producto.Fotos,
                Artesano = producto.artesano?.nombre,
                SubCategoria = producto.SubCategoria?.Nombre,
                ArtesanoId = producto.artesano?.id ?? 0,
                Reporte = new AgregarReporteDto(),
                PromedioCalificacion = (double)promedio,
                CantidadReseñas = cantidad
            };
        }
        // GET: ProductoController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MostrarProdcutosProvicional()
        {
            var email = HttpContext.Session.GetString("loginUsuario");

            if (email != null)
            {
                var usuario = _obtenerUsuario.Ejecutar(email);
                var itemsCarrito = _mostrarProductosCarrito.mostrarProductos(usuario.id);

                ViewBag.CantidadProductos = itemsCarrito.Sum(i => i.cantidad);
            }
            else
            {
                ViewBag.CantidadProductos = 0;
            }
            IEnumerable<Producto> productos = _obtenerTodosLosProductos.obtenerTodos();
            var modelo = new ProductosProvicionalesModel
            {
                Productos = productos
            };
            return View(modelo);
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
        [HttpGet]
        public IActionResult ObtenerSubCategorias(int categoriaId)
        {
            var subcategorias = _obtenerSubCategoria.obtenerPorCtegoria(categoriaId);

            return Json(subcategorias.Select(s => new {
                id = s.Id,
                nombre = s.Nombre
            }));
        }

       
      

    }
}
