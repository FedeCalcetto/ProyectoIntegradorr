using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Excepciones;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador.LogicaNegocio.ValueObjects;
using ProyectoIntegrador_Web.Models;
using ProyectoIntegrador_Web.Services;

namespace ProyectoIntegrador_Web.Controllers
{
    public class ArtesanoController : Controller
    {
        // GET: ArtesanoController
        private readonly IArtesanoRepositorio _artesanorepo;
        private readonly ICategoriaRepositorio _categoria;
        private readonly ISubCategoriaRepositorio _SubCategoria;
        private readonly IProductoRepositorio _producto;
        private readonly IObtenerArtesano _obtenerArtesano;
        private readonly EmailService _email;
        private readonly IEliminarProducto _eliminarProducto;
        private readonly IProductoFotoRepsoitorio _productoFoto;
        private readonly IEditarProducto _editarProducto;
        public ArtesanoController(IArtesanoRepositorio artesanorepo, ISubCategoriaRepositorio subCategoria,ICategoriaRepositorio categoria, IProductoRepositorio producto, IEliminarProducto eliminarProducto, IProductoFotoRepsoitorio productoFoto,IEditarProducto editarProducto, EmailService email, IObtenerArtesano obtenerArtesano)
        {
            _artesanorepo = artesanorepo;
            _SubCategoria = subCategoria;
            _categoria = categoria;
            _producto = producto;
            _email = email;
            _obtenerArtesano = obtenerArtesano;
            _eliminarProducto = eliminarProducto;
            _productoFoto = productoFoto;
            _editarProducto = editarProducto;
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
        


        // GET: ArtesanoController/Details/5
        public ActionResult GetSubcategorias(int categoriaId)
        {
            var subcategorias = _SubCategoria.ObtenerTodos()
            .Where(s => s.categoriaId == categoriaId)
            .Select(s => new {
            id = s.Id,
            nombre = s.Nombre
            });

            return Json(subcategorias);
        }
        public IActionResult AltaProducto()
        {
            // Obtener el email del usuario logueado
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
            var model = new AltaProductoViewModel();
            model.Categorias = _categoria.ObtenerTodos(); // Cargar categorías
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AltaProducto(AltaProductoViewModel modelo)
        {
            var email = HttpContext.Session.GetString("loginUsuario");

            if (!ModelState.IsValid)
            {
                modelo.Categorias = _categoria.ObtenerTodos();
                modelo.SubCategorias = _SubCategoria.ObtenerTodas();
                return View(modelo);
            }

            var artesano = _artesanorepo.ObtenerPorEmail(email);
            if (artesano == null)
                return NotFound();


            string nombreArchivo = null;

            if (modelo.ArchivoImagen != null && modelo.ArchivoImagen.Length > 0)
            {
                var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/productos");

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                nombreArchivo = Guid.NewGuid() + Path.GetExtension(modelo.ArchivoImagen.FileName);
                var ruta = Path.Combine(carpeta, nombreArchivo);

                using (var stream = new FileStream(ruta, FileMode.Create))
                {
                    modelo.ArchivoImagen.CopyTo(stream);
                }
            }

            var entidad = new Producto
            {
                nombre = modelo.nombre,
                descripcion = modelo.descripcion,
                precio = modelo.precio,
                stock = modelo.stock,
                imagen = nombreArchivo,
                artesano = artesano,
                SubCategoriaId = (int)modelo.SubCategoriaId,
                Fotos = new List<ProductoFoto>()   
            };

            if (modelo.Fotos != null && modelo.Fotos.Any())
            {
                string carpetaMultiples = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/productos");
                Directory.CreateDirectory(carpetaMultiples);

                foreach (var foto in modelo.Fotos)
                {
                    if (foto != null && foto.Length > 0)
                    {
                        string nombre = Guid.NewGuid() + Path.GetExtension(foto.FileName);
                        var ruta = Path.Combine(carpetaMultiples, nombre);

                        using (var stream = new FileStream(ruta, FileMode.Create))
                        {
                            foto.CopyTo(stream);
                        }

                        entidad.Fotos.Add(new ProductoFoto
                        {
                            UrlImagen = nombre
                        });
                    }
                }
            }

            try
            {
                _producto.Agregar(entidad);
                artesano.productos.Add(entidad);
                modelo.Categorias = _categoria.ObtenerTodos();
                modelo.SubCategorias = _SubCategoria.ObtenerTodas();
                TempData["Mensaje"] = "Producto agregado correctamente.";
                return RedirectToAction("AltaProducto");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                modelo.Categorias = _categoria.ObtenerTodos();
                modelo.SubCategorias = _SubCategoria.ObtenerTodas();
                return View(modelo);
            }



        }

        public IActionResult ProductosDelArtesano()
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            var rol = HttpContext.Session.GetString("Rol")?.Trim().ToUpper();

            if (string.IsNullOrEmpty(email) || rol != "ARTESANO")
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
        public IActionResult EditarProducto(int id)
        {

            var email = HttpContext.Session.GetString("loginUsuario");
            var rol = HttpContext.Session.GetString("Rol")?.Trim().ToUpper();

            if (string.IsNullOrEmpty(email) || rol != "ARTESANO")
            {
                return RedirectToAction("Login", "Login");
            }

            Producto producto = _producto.Obtener(id);

            var modelo = new EditarProductoViewModel
            {
                Id = producto.id,
                nombre = producto.nombre,
                descripcion = producto.descripcion,
                precio = producto.precio,
                stock = producto.stock,
                SubCategoriaId = producto.SubCategoriaId,
                Categorias = _categoria.ObtenerTodos(),
                SubCategorias = _SubCategoria.ObtenerTodas()
            };
            ViewBag.Id = id;
            return View(modelo);

        }
        [HttpPost]
        public IActionResult EditarProducto(EditarProductoViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                modelo.Categorias = _categoria.ObtenerTodos();
                modelo.SubCategorias = _SubCategoria.ObtenerTodas();
                return View(modelo);
            }

            Producto producto = _producto.Obtener(modelo.Id);

            if (producto == null)
                return NotFound();

            producto.nombre = modelo.nombre;
            producto.descripcion = modelo.descripcion;
            producto.precio = modelo.precio;
            producto.stock = modelo.stock;
            producto.SubCategoriaId = (int)modelo.SubCategoriaId;

            try
            {
                _editarProducto.Ejecutar(producto);

                TempData["Mensaje"] = "Producto actualizado correctamente.";
                return RedirectToAction("EditarProducto", new { id = modelo.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);

                modelo.Categorias = _categoria.ObtenerTodos();
                modelo.SubCategorias = _SubCategoria.ObtenerTodas();

                return View(modelo);
            }
        }

        public IActionResult EliminarProducto(int id)
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            var rol = HttpContext.Session.GetString("Rol")?.Trim().ToUpper();

            if (string.IsNullOrEmpty(email) || rol != "ARTESANO")
            {
                return RedirectToAction("Login", "Login");
            }
            Producto producto = _producto.Obtener(id);

            var modelo = new EliminarProductoViewModel
            {
                nombre = producto.nombre,
                descripcion = producto.descripcion
            };

            ViewBag.Id = id;
            return View(modelo);
        }


        [HttpPost]
        public IActionResult EliminarProductoConfirmado(int id)
        {
            //_producto.Eliminar(id);
            _eliminarProducto.Ejecutar(id);
            return RedirectToAction("ProductosDelArtesano");
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
            var artesano = _obtenerArtesano.Ejecutar(email);
            _artesanorepo.Eliminar(artesano.id);

            // limpiar sesión
            HttpContext.Session.Clear();

            return RedirectToAction("CuentaEliminada");
        }

    }
}
