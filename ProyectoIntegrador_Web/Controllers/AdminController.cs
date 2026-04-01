using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador_Web.Models;
using ProyectoIntegrador_Web.Services;


namespace ProyectoIntegrador_Web.Controllers
{
    public class AdminController : Controller
    {
        public readonly IListadoDeReportes _listadoDeReportes;
        public readonly IEliminarReporte _eliminarReporte;
        public readonly IEliminarProducto _eliminarProducto;
        public readonly IObtenerReporte _obtenerReporte;
        public readonly IArtesanoRepositorio _artesanoRepositorio;
        public readonly IBloquearArtesano _bloquearArtesano;
        private readonly EmailService _email;
        public AdminController(IListadoDeReportes listadoDeReportes, IEliminarReporte eliminarReporte, IEliminarProducto eliminarProducto, IObtenerReporte obtenerReporte, IArtesanoRepositorio eliminarArtesano, IBloquearArtesano bloquearArtesano, EmailService email)
        {
            _listadoDeReportes = listadoDeReportes;
            _eliminarReporte = eliminarReporte;
            _eliminarProducto = eliminarProducto;
            _obtenerReporte = obtenerReporte;
            _artesanoRepositorio = eliminarArtesano;
            _bloquearArtesano = bloquearArtesano;
            _email = email;
        }


        // GET: AdminController
        public ActionResult Inicio()
        {
            if (HttpContext.Session.GetString("Rol") != null &&
                HttpContext.Session.GetString("Rol").Trim().ToUpper() == "ADMIN")
            {
                var reportes = _listadoDeReportes.Ejecutar();

                var vm = new ListadoDeReportesViewModel
                {
                    Reportes = reportes
                };

                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarReporte(int id)
        {
            _eliminarReporte.Ejecutar(id);
            return RedirectToAction("Inicio");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AceptarReporte(int id)
        {
            // Obtener el reporte
            var reporte = _obtenerReporte.Ejecutar(id);

            if (reporte == null)
            {
                TempData["Error"] = "No se encontró el reporte.";
                return RedirectToAction("Inicio");
            }

            var artesanoId = reporte.artesano?.id;
            var productoId = reporte.producto?.id;

            // Si el reporte es para un producto
            if (artesanoId == null && productoId != null)
            {
                // Guardar datos antes de eliminar por seguridad
                var usuarioEmail = reporte.producto?.artesano?.email?.ToString();
                var nombreProducto = reporte.producto?.nombre;

                // Eliminar el producto
                _eliminarProducto.Ejecutar(productoId.Value);

                // Enviar correo al usuario informando de la eliminación del producto
                if (!string.IsNullOrEmpty(usuarioEmail) && !string.IsNullOrEmpty(nombreProducto))
                {
                    await _email.EnviarAvisoProductoEliminadoAsync(usuarioEmail, nombreProducto);
                }

                // Si también querés eliminar el reporte
                // _eliminarReporte.Ejecutar(id);
            }
            // Si el reporte es para un artesano
            else if (artesanoId != null && productoId == null)
            {
                // Eliminar el reporte
                _eliminarReporte.Ejecutar(id);

                // Bloquear al artesano
                _bloquearArtesano.bloquearArtesano(artesanoId.Value);
            }

            return RedirectToAction("Inicio");
        }
        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
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

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
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

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
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
