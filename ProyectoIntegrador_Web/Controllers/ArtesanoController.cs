using Humanizer;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ProyectoIntegrador.LogicaAplication.CasosDeUso;
using ProyectoIntegrador.LogicaAplication.Dtos;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Excepciones;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador.LogicaNegocio.ValueObjects;
using ProyectoIntegrador_Web.Models;
using ProyectoIntegrador_Web.Services;
using System;
using System.Linq;

namespace ProyectoIntegrador_Web.Controllers
{
    public class ArtesanoController : Controller
    {
        // GET: ArtesanoController
            private readonly EmailService _email;
            private readonly IEditarProducto _editarProducto;
            private readonly IWebHostEnvironment _env;
            private readonly IEditarArtesano _editarArtesano;
            private readonly IEliminarProducto _eliminarProducto;
            private readonly IObtenerArtesano _obtenerArtesano;
            private readonly IObtenerCategorias _obtenerCategorias;
            private readonly IObtenerSubcategorias _obtenerSubCategorias;
            private readonly IObtenerProductoArtesano _obtenerProductoArtesano;
            private readonly IObtenerProducto _obtenerProducto;
            private readonly IEliminarArtesano _eliminarArtesano;
            private readonly IProductoEstaEnCarrito _productoEstaEnCarrito;
            private readonly IFacturaRepositorio _facturaRepo;
        private readonly IDashboard _dashboard;

        public ArtesanoController(
                IArtesanoRepositorio artesanoRepo,
                IObtenerProducto obtenerProducto,
                IEditarArtesano editarArtesano,
                IWebHostEnvironment env,
                IObtenerArtesano obtenerArtesano,
                IObtenerCategorias obtenerCategorias,
                IObtenerSubcategorias obtenerSubCategorias,
                IObtenerProductoArtesano obtenerProductoArtesano,
                IEliminarProducto eliminarProducto,
                IProductoFotoRepsoitorio productoFoto,
                IEditarProducto editarProducto,
                IEliminarArtesano eliminarArtesano,
                IFacturaRepositorio facturaRepo,
                IProductoEstaEnCarrito productoEstaEnCarrito,
                IDashboard dashboard,
                EmailService email
        )
        {
                _obtenerProducto = obtenerProducto;
                _editarArtesano = editarArtesano;
                _env = env;
                _obtenerArtesano = obtenerArtesano;
                _obtenerCategorias = obtenerCategorias;
                _obtenerSubCategorias = obtenerSubCategorias;
                _obtenerProductoArtesano = obtenerProductoArtesano;
                _eliminarProducto = eliminarProducto;
                _editarProducto = editarProducto;
                _eliminarArtesano = eliminarArtesano;
                _facturaRepo = facturaRepo;
                _productoEstaEnCarrito = productoEstaEnCarrito;
                _email = email;
            }
       
                _dashboard = dashboard;
        }
        
        public ActionResult Inicio()
        {
            if (HttpContext.Session.GetString("Rol") != null && HttpContext.Session.GetString("Rol").Trim().ToUpper().Equals("ARTESANO"))
            {
                var email = HttpContext.Session.GetString("loginUsuario");
                var facturas = _facturaRepo.ObtenerPorArtesano(email);
                return View(facturas);
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
                Foto = artesano.foto,

                EliminarCuenta = new EliminarCuentaViewModel
                {
                    Email = artesano.email.email
                }

            };

            return View(modelo);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PerfilArtesano(EditarArtesanoViewModel modelo, IFormFile archivoFoto)
        {
            try
            {
                var email = HttpContext.Session.GetString("loginUsuario");

                modelo.EliminarCuenta ??= new EliminarCuentaViewModel //para que el modal no se pierda si surge error
                {
                    Email = email
                };


                ModelState.Remove("archivoFoto");

                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "No se puedo actualizar el perfil.";
                    return View(modelo);
                }

                var artesano = _obtenerArtesano.Ejecutar(email);
                if (artesano == null)
                {
                    return NotFound("No se encontró el artesano para actualizar.");
                }

                try
                {
                    var nombreArchivo = "";
                    //   SI SUBIÓ FOTO NUEVA
                    if (archivoFoto != null && archivoFoto.Length > 0)
                    {
                        var tiposPermitidos = new[] { "image/jpeg", "image/png", "image/jpg" };
                        if (!tiposPermitidos.Contains(archivoFoto.ContentType)) //validamos el tipo de imagen
                        {
                            TempData["Error"] = "El archivo debe ser una imagen JPG o PNG.";
                            return RedirectToAction("PerfilArtesano");
                        }

                        var extension = Path.GetExtension(archivoFoto.FileName).ToLower(); //GetExtension trae la extensión del archivo
                        var extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png" };

                        if (!extensionesPermitidas.Contains(extension))
                        {
                            TempData["Error"] = "Formato no permitido. Usa JPG o PNG.";
                            return RedirectToAction("PerfilArtesano");
                        }

                        nombreArchivo = Guid.NewGuid() + extension; //NewGuid crea un identificador único
                        var uploads = Path.Combine(_env.WebRootPath, "images/usuarios");
                        //_env.WebRootPath: ruta física a wwwroot en el servidor (ej. C:\app\wwwroot o /home/site/wwwroot).
                        //Path combine construye la ruta donde guardar las imgs

                        var filePath = Path.Combine(uploads, nombreArchivo); //Contruye la ruta física del archivo

                        using (var stream = new FileStream(filePath, FileMode.Create))
                            archivoFoto.CopyTo(stream);

                        //️ _env.WebRootPath = C:\Proyecto\wwwroot
                        //"images/usuarios" =  C:\Proyecto\wwwroot\images\usuarios (uploads)
                        //nombreArchivo=  C:\Proyecto\wwwroot\images\usuarios\foto123.jpg   (filePath)
                    }

                    var dto = new EditarArtesanoDto
                    {
                        Descripcion = modelo.Descripcion,
                        Foto = string.IsNullOrEmpty(nombreArchivo)
                        ? modelo.Foto
                        : "/images/usuarios/" + nombreArchivo,
                        Telefono = modelo.Telefono,
                        Nombre = modelo.Nombre,
                        Apellido = modelo.Apellido,
                        Email = modelo.Email,
                        Password = modelo.Password
                    };

                    _editarArtesano.Actualizar(dto);

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
            catch (Exception ex)
            {
                // Si querés podés manejar excepciones acá
                TempData["Error"] = "Error inesperado.";
                return View(modelo);
            }
        } 



        public IActionResult ProductosDelArtesano()
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            var rol = HttpContext.Session.GetString("Rol")?.Trim().ToUpper();
            var artesano = _obtenerArtesano.Ejecutar(email);
            if (string.IsNullOrEmpty(email) || rol != "ARTESANO")
            {
                return RedirectToAction("Login", "Login");
            }

            // Obtener los productos del artesano
            var artesanoProductos =  _obtenerProductoArtesano.obtenerProductos(email);

            // Crear el modelo para la vista
            var modelo = new ProductosDelArtesanoViewModel
            {
                Productos = artesanoProductos.productos
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

            Producto producto = _obtenerProducto.obtener(id);

            var modelo = new EditarProductoViewModel
            {
                Id = producto.id,
                nombre = producto.nombre,
                descripcion = producto.descripcion,
                precio = producto.precio,
                stock = producto.stock,
                imagen = producto.imagen,
                FotosActuales = producto.Fotos,
                SubCategoriaId = producto.SubCategoriaId,
                SubCategorias = _obtenerSubCategorias.obtenerTodas(),
                 CategoriaId = producto.SubCategoria.categoriaId,
                Categorias = _obtenerCategorias.ObtenerTodos()
            };
            ViewBag.Id = id;
            return View(modelo);

        }
        [HttpPost]
        public IActionResult EditarProducto(EditarProductoViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                modelo.Categorias = _obtenerCategorias.ObtenerTodos();
                modelo.SubCategorias = _obtenerSubCategorias.obtenerTodas();
                return View(modelo);
            }

            Producto producto = _obtenerProducto.obtener(modelo.Id);

            if (producto == null)
                return NotFound();


            string imagenPrincipal = producto.imagen;
            if (modelo.ArchivoImagen != null && modelo.ArchivoImagen.Length > 0)
            {
                string nombreImagen = Guid.NewGuid() + Path.GetExtension(modelo.ArchivoImagen.FileName);
                string ruta = Path.Combine(_env.WebRootPath, "images/productos", nombreImagen);

                using (var stream = new FileStream(ruta, FileMode.Create))
                {
                    modelo.ArchivoImagen.CopyTo(stream);
                }

                imagenPrincipal = nombreImagen;
                modelo.imagen = imagenPrincipal;
            }
            var fotosNuevas = new List<string>();
            fotosNuevas.Add(imagenPrincipal);
            if (modelo.FotosNuevas != null)
            {
                foreach (var foto in modelo.FotosNuevas)
                {
                    if (foto != null && foto.Length > 0)
                    {
                        string nombre = Guid.NewGuid() + Path.GetExtension(foto.FileName);
                        string ruta = Path.Combine(_env.WebRootPath, "images/productos", nombre);

                        using (var stream = new FileStream(ruta, FileMode.Create))
                        {
                            foto.CopyTo(stream);
                        }

                        fotosNuevas.Add(nombre);
                    }
                }
            }

            //----------------------------------------------------
            // ARMAR DTO PARA CU
            //----------------------------------------------------
            var dto = new EditarProductoDto
            {
                Id = modelo.Id,
                Nombre = modelo.nombre,
                Descripcion = modelo.descripcion,
                Precio = modelo.precio,
                Stock = modelo.stock,
                SubCategoriaId = modelo.SubCategoriaId,

                ImagenPrincipal = modelo.imagen,
                Fotos = fotosNuevas
            };

            try
            {
                _editarProducto.Ejecutar(dto);

                TempData["Mensaje"] = "Producto actualizado correctamente.";
                return RedirectToAction("EditarProducto", new { id = modelo.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                modelo.Categorias = _obtenerCategorias.ObtenerTodos();
                modelo.SubCategorias = _obtenerSubCategorias.obtenerTodas();
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
            Producto producto = _obtenerProducto.obtener(id); 

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
            if (_productoEstaEnCarrito.estaEnCarrito(id))
            {
                TempData["Error"] = "No se puede eliminar el producto porque está en un carrito.";
                return RedirectToAction("ProductosDelArtesano");
            }

            _eliminarProducto.Ejecutar(id);
            return RedirectToAction("ProductosDelArtesano");
        }

        public IActionResult Dashboard()
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            var rol = HttpContext.Session.GetString("Rol")?.Trim().ToUpper();

            if (string.IsNullOrEmpty(email) || rol != "ARTESANO")
            {
                return RedirectToAction("Login", "Login");
            }
            var dashboardDto = _dashboard.Ejecutar(email, 12);

            var viewModel = new DashboardViewModel
            {
                CantidadVentasTotal = dashboardDto.CantidadVentasTotal,
                CantidadVentasMesActual = dashboardDto.CantidadVentasMesActual,
                CantidadVentasAnoActual = dashboardDto.CantidadVentasAnoActual,
                VariacionVentasMensual = dashboardDto.VariacionVentasMensual,
                TopProductosVentas = dashboardDto.TopProductosVentas,
                GraficoVentas = dashboardDto.GraficoVentas
            };

            return View(viewModel);
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult NuevaPassword(NuevaPasswordViewModel modelo)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        return View(modelo);

        //    var email = HttpContext.Session.GetString("loginUsuario");
        //    var artesano = _obtenerArtesano.Ejecutar(email);

        //    if (artesano == null)
        //        return NotFound();

            
        //        artesano.password = modelo.Password;
        //        _artesanorepo.Actualizar(artesano);

        //        TempData["Mensaje"] = "Contraseña actualizada correctamente.";
        //        return RedirectToAction("PerfilArtesano");
        //    }
        //    catch (MayusculaPasswordException ex)
        //    {
        //        ModelState.AddModelError(string.Empty, ex.Message);

        //        return View(modelo);
        //    }
        //    catch (numeroPassowordException ex)
        //    {
        //        ModelState.AddModelError(string.Empty, ex.Message);

        //        return View(modelo);
        //    }
        //    catch (passwordUsuarioException ex)
        //    {
        //        ModelState.AddModelError(string.Empty, ex.Message);

        //        return View(modelo);
        //    }
        //}

        //[HttpPost]
        //public IActionResult CambiarFoto(IFormFile archivoFoto)
        //{
        //    var email = HttpContext.Session.GetString("loginUsuario");
        //    var artesano = _artesanorepo.ObtenerPorEmail(email);

        //    if (archivoFoto == null || archivoFoto.Length == 0)
        //    {
        //        TempData["Error"] = "Debes seleccionar un archivo.";
        //        return RedirectToAction("PerfilArtesano");
        //    }
        //    var tiposPermitidos = new[] { "image/jpeg", "image/png", "image/jpg" };
        //    if (!tiposPermitidos.Contains(archivoFoto.ContentType))
        //    {
        //        TempData["Error"] = "El archivo debe ser una imagen JPG o PNG.";
        //        return RedirectToAction("PerfilArtesano");
        //    }

        //    var extension = Path.GetExtension(archivoFoto.FileName).ToLower();
        //    var extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png" };

        //    if (!extensionesPermitidas.Contains(extension))
        //    {
        //        TempData["Error"] = "Formato no permitido. Usa JPG o PNG.";
        //        return RedirectToAction("PerfilArtesano");
        //    }

        //    var nombreArchivo = Guid.NewGuid() + extension;
        //    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/usuarios");

        //    if (!Directory.Exists(uploads))
        //    {
        //        Directory.CreateDirectory(uploads);
        //    }

        //    var filePath = Path.Combine(uploads, nombreArchivo);

        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        archivoFoto.CopyTo(stream);
        //    }

        //    artesano.foto = "/images/usuarios/" + nombreArchivo;
        //    _artesanorepo.Actualizar(artesano);

        //    TempData["Mensaje"] = "¡Foto actualizada con éxito!";
        //    return RedirectToAction("PerfilArtesano");
        //}

        public IActionResult CuentaEliminada()
        {
            return View();
        }

        // GET: solo muestra la vista
        public IActionResult ConfirmarEliminacion()
        {
            var email = HttpContext.Session.GetString("loginUsuario");

            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Login");

            return View(new EliminarCuentaViewModel
            {
                Email = email
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PedirCodigoEliminar()
        {
            var email = HttpContext.Session.GetString("loginUsuario");

            if (string.IsNullOrEmpty(email))
                return Unauthorized();

            string codigo = new Random().Next(100000, 999999).ToString();

            HttpContext.Session.SetString("CodigoEliminar_" + email, codigo);
            HttpContext.Session.SetString(
                "EliminarExpira_" + email,
                DateTime.Now.AddMinutes(10).ToString()
            );

            await _email.EnviarCodigoAsync(email, codigo, "eliminacion", null);

            return Ok();
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
            _eliminarArtesano.Ejecutar(artesano.email.email);

            // limpiar sesión
            HttpContext.Session.Clear();

            return RedirectToAction("CuentaEliminada");
        }

    }
}
