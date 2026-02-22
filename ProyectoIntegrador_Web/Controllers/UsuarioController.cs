using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using ProyectoIntegrador.LogicaAplication.Dtos;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Excepciones;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador_Web.Models;
using ProyectoIntegrador_Web.Services;

namespace ProyectoIntegrador_Web.Controllers
{
    public class UsuarioController : Controller
    {


        private readonly ICambiarPassword _cambiarPassword;
        private readonly IEliminarUsuario _eliminarUsuario;
        private readonly EmailService _email;
        private readonly ICatalogoService _catalogoService;
        private readonly IBusquedaDeUsuarios _busquedaDeUsuarios;
        private readonly IObtenerCategorias _obtenerCategorias;
        private readonly IObtenerArtesanoId _obtenerArtesano;
        private readonly IObtenerCliente _obtenerCliente;
        private readonly IAgregarReporte _agregarReporte;
        private readonly IUsuarioRepositorio _usuarioRepo;
        private readonly IClienteRepositorio _clienteRepo;
        private readonly IObtenerClienteConFavoritos _obtenerClienteConFav;

        public UsuarioController(ICambiarPassword cambiarPassword,IEliminarUsuario eliminarUsuario,EmailService email, ICatalogoService catalogoService,IBusquedaDeUsuarios busquedaDeUsuarios, IObtenerCategorias obtenerCategorias, IObtenerArtesanoId obtenerArtesanoId,IObtenerCliente obtenerCliente,IAgregarReporte agregarReporte,IUsuarioRepositorio usuarioRepo,IClienteRepositorio clienteRepo, IObtenerClienteConFavoritos obtenerClienteConFav)
        private readonly IAgregarComentario _agregarComentario;

        public UsuarioController(ICambiarPassword cambiarPassword,IEliminarUsuario eliminarUsuario,EmailService email, ICatalogoService catalogoService,IBusquedaDeUsuarios busquedaDeUsuarios, IObtenerCategorias obtenerCategorias, IObtenerArtesanoId obtenerArtesanoId,IObtenerCliente obtenerCliente,IAgregarReporte agregarReporte,IUsuarioRepositorio usuarioRepo,IClienteRepositorio clienteRepo,IAgregarComentario agregarComentario)
        {
            _cambiarPassword = cambiarPassword;
            _eliminarUsuario = eliminarUsuario;
            _email = email;
            _catalogoService = catalogoService;
            _busquedaDeUsuarios = busquedaDeUsuarios;
            _obtenerCategorias = obtenerCategorias;
            _obtenerArtesano = obtenerArtesanoId;
            _obtenerCliente = obtenerCliente;
            _agregarReporte = agregarReporte;
            _usuarioRepo = usuarioRepo;
            _clienteRepo = clienteRepo;
            _obtenerClienteConFav = obtenerClienteConFav;
            _agregarComentario = agregarComentario;
        }


        public IActionResult CambioContra(string returnUrl)
            {
            ViewBag.ReturnUrl = returnUrl ?? "/"; 
            return View();
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CambioContra(CambioPassowrdViewModel modelo, string returnUrl)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(modelo);
            }
            var email = HttpContext.Session.GetString("loginUsuario");

            if (email == null)
                return RedirectToAction("Login", "Login");

            try
            {
                _cambiarPassword.Ejecutar(modelo.Password, modelo.PasswordRepetida, modelo.passwordActual, email);

                TempData["Mensaje"] = "Contraseña actualizada correctamente.";

                return Redirect(returnUrl ?? "/");
            }
            catch (MayusculaPasswordException ex)
            {
                ModelState.AddModelError("", "La contraseña debe contener mayúsucla");
                ViewBag.ReturnUrl = returnUrl;
                return View(modelo);
            }
            catch (ContraActualException ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.ReturnUrl = returnUrl;
                return View(modelo);
            }
            catch (NoCoincideException ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.ReturnUrl = returnUrl;
                return View(modelo);
            }
            catch (SonIgualesException ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.ReturnUrl = returnUrl;
                return View(modelo);
            }
            catch (numeroPassowordException ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.ReturnUrl = returnUrl;
                return View(modelo);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error inesperado.");
                ViewBag.ReturnUrl = returnUrl;
                return View(modelo);
            }
          
            
        }

        public IActionResult BusquedaDeUsuarios(string filtro)
        {

            var email = HttpContext.Session.GetString("loginUsuario");
            var rol = HttpContext.Session.GetString("Rol")?.Trim().ToUpper();

           if (string.IsNullOrEmpty(email))
           {
                return RedirectToAction("Login", "Login");
           }
           var usuario = _usuarioRepo.BuscarPorEmail(email);
            var modelo = new BusquedaDeUsuariosViewModel
            {
                Filtro = filtro,
                usuarioLogueado = usuario
            };

            // Ejecutar búsqueda solo si hay filtro
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                modelo.Usuarios = _busquedaDeUsuarios.Ejecutar(filtro);
            }

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Seguir(int artesanoId)
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Login");

            var cliente = _usuarioRepo.BuscarPorEmail(email) as Cliente;
            if (cliente == null)
                return Unauthorized();

            var artesano = _obtenerArtesano.Ejecutar(artesanoId);
            if (artesano == null)
                return NotFound();

            _clienteRepo.agregarArtesano(cliente, artesano);

            return RedirectToAction("BusquedaDeUsuarios", "Usuario");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DejarDeSeguir(int artesanoId)
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Login");

            var cliente = _usuarioRepo.BuscarPorEmail(email) as Cliente;
            if (cliente == null)
                return Unauthorized();

            var artesano = _obtenerArtesano.Ejecutar(artesanoId);
            if (artesano == null)
                return NotFound();

            _clienteRepo.eliminarArtesano(cliente, artesano);

            return RedirectToAction("BusquedaDeUsuarios", "Usuario");
        }

        public IActionResult PerfilPublico(int id)
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            var rol = HttpContext.Session.GetString("Rol")?.Trim().ToUpper();

             if (string.IsNullOrEmpty(email) || (rol != "ARTESANO" && rol != "CLIENTE" && rol != "ADMIN"))
             {
                 return RedirectToAction("Login", "Login");
             }

            var artesano = _obtenerArtesano.Ejecutar(id);

            if (artesano == null)
                return NotFound();

            var vm = new PerfilPublicoViewModel
            {
                Artesano = artesano,
                Productos = artesano.productos
            };

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReportarArtesano(int artesanoId, AgregarReporteDto reporte)
        {
            if (!ModelState.IsValid)
            {
                var vm = CrearDetalleVM(artesanoId);
                vm.Reporte = reporte;
                return View("PerfilPublico", vm);
            }

            var email = HttpContext.Session.GetString("loginUsuario");
            var cliente = _obtenerCliente.Ejecutar(email);
            var artesano = _obtenerArtesano.Ejecutar(artesanoId);

            if (cliente == null || artesano == null)
                return NotFound();

            try
            {
                _agregarReporte.Ejecutar(
                    reporte,
                    artesano: artesano,
                    cliente: cliente,
                    producto: null
                );

                TempData["Mensaje"] = "Reporte enviado correctamente";
                return RedirectToAction("PerfilPublico", new { id = artesanoId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);

                var vm = CrearDetalleVM(artesanoId);
                vm.Reporte = reporte;
                return View("PerfilPublico", vm);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ComentarArtesano(int clienteId, int artesanoId, AgregarComentarioDto comentario)
        {
            if (!ModelState.IsValid)
            {
                var vm = CrearDetalleVM(artesanoId);
                vm.Comentario = comentario;
                return View("PerfilPublico", vm);
            }

            var email = HttpContext.Session.GetString("loginUsuario");
            var cliente = _obtenerCliente.Ejecutar(email);
            var artesano = _obtenerArtesano.Ejecutar(artesanoId);

            if (cliente == null || artesano == null)
                return NotFound();

            try
            {
                _agregarComentario.Ejecutar(
                    comentario,
                    c: cliente,
                    a: artesano
                );

                TempData["Mensaje"] = "Comentario enviado correctamente";
                return RedirectToAction("PerfilPublico", new { id = artesanoId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);

                var vm = CrearDetalleVM(artesanoId);
                vm.Comentario = comentario;
                return View("PerfilPublico", vm);
            }
        }

        private PerfilPublicoViewModel CrearDetalleVM(int id)
        {
            var artesano = _obtenerArtesano.Ejecutar(id);

            return new PerfilPublicoViewModel
            {
                Artesano = artesano,
                Productos = artesano.productos,
                Reporte = new AgregarReporteDto(),
                Comentario = new AgregarComentarioDto()
            };
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult BusquedaDeUsuarios(BusquedaDeUsuariosViewModel modelo)
        //{

        //    modelo.Usuarios = _busquedaDeUsuarios.Ejecutar(modelo.Filtro);

        //    return View(modelo);
        //}

        
        // GET: UsuarioController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
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

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioController/Edit/5
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

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
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

        // eliminacion del usuario

        public IActionResult CuentaEliminada()
        {
            return View();
        }

        /*  public async Task<IActionResult> ConfirmarEliminacion()  esto se muda al get, para que trabaje con el MODAL
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
          }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarCuenta(EliminarCuentaViewModel modelo)
        {
            var emailSesion = HttpContext.Session.GetString("loginUsuario");// por seguridad se toa el mail de session no del model(puede ser usado por erramientas externas DevTools y cambiarlo)

            if (string.IsNullOrEmpty(emailSesion))
            {
                return RedirectToAction("Login", "Login");
            }

            var codigoIngresado = modelo.Codigo;

            var codigoGuardado = HttpContext.Session.GetString("CodigoEliminar_" + emailSesion);
            var expiraStr = HttpContext.Session.GetString("EliminarExpira_" + emailSesion);

            // ❌ No hay código o expiró
            if (codigoGuardado == null || expiraStr == null)
            {
                TempData["ErrorEliminar"] = "El código expiró. Pedí uno nuevo.";
                return VolverAlPerfilSegunRol();
            }

            if (!DateTime.TryParse(expiraStr, out var expira) || DateTime.Now > expira)
            {
                TempData["ErrorEliminar"] = "El código expiró.";
                return VolverAlPerfilSegunRol();
            }

            // ❌ Código incorrecto
            if (codigoGuardado != codigoIngresado)
            {
                TempData["ErrorEliminar"] = "Código incorrecto.";
                return VolverAlPerfilSegunRol();
            }

            // ✅ Código correcto
            _eliminarUsuario.Ejecutar(emailSesion);

            HttpContext.Session.Clear();

            return RedirectToAction("CuentaEliminada");
        }

        //////////////////////////////////////////////////////////////////
        ////////////////////// catalogo de usuarios //////////////////////
        //////////////////////////////////////////////////////////////////

        public IActionResult CatalogoUsuarios()
        {
            return View();

        }

        public IActionResult Catalogo()
        {
            // DTO de la capa aplicación
            var catalogoDto = _catalogoService.ObtenerCatalogoInicial();

            // favoritos del cliente logueado
            var rol = HttpContext.Session.GetString("Rol")?.ToUpper();
            var email = HttpContext.Session.GetString("loginUsuario");

            var favoritosIds = new HashSet<int>();

            if (rol == "CLIENTE" && !string.IsNullOrEmpty(email))
            {
                var cliente = _obtenerClienteConFav.Ejecutar(email);

                favoritosIds = cliente?.productosFavoritos?
                    .Select(p => p.id) 
                    .ToHashSet()
                    ?? new HashSet<int>();
            }

            //  marcar EsFavorito en Recientes
            if (catalogoDto?.Recientes != null)
            {
                foreach (var p in catalogoDto.Recientes)
                    p.EsFavorito = favoritosIds.Contains(p.Id);
            }

            //  marcar EsFavorito en PorCategoria
            if (catalogoDto?.PorCategoria != null)
            {
                foreach (var cat in catalogoDto.PorCategoria)
                {
                    if (cat?.Productos == null) continue;

                    foreach (var p in cat.Productos)
                        p.EsFavorito = favoritosIds.Contains(p.Id);
                }
            }

            // ViewModel de la capa Web, esto carga talalogo
            var vm = new CatalogoViewModel
            {
                Catalogo = catalogoDto,
                Buscador = new ProductosFiltradosViewModel
                {
                    Categorias = _obtenerCategorias.ObtenerTodos(),
                    ModoBusqueda = "productos"
                }
            };

            return View(vm);
        }   


        private IActionResult VolverAlPerfilSegunRol()
        {
            var rol = HttpContext.Session.GetString("Rol");

            if (rol == "CLIENTE")
                return RedirectToAction("Perfil", "Cliente");

            if (rol == "Artesano")
                return RedirectToAction("PerfilArtesano", "Artesano");

            return RedirectToAction("Login", "Login");
        }

    }
}
