using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador_Web.Models;

namespace ProyectoIntegrador_Web.Controllers
{
    public class AdminController : Controller
    {
        public readonly IListadoDeReportes _listadoDeReportes;
        public readonly IEliminarReporte _eliminarReporte;
        public readonly IEliminarProducto _eliminarProducto;
        public readonly IObtenerReporte _obtenerReporte;
        public AdminController(IListadoDeReportes listadoDeReportes, IEliminarReporte eliminarReporte, IEliminarProducto eliminarProducto, IObtenerReporte obtenerReporte)
        {
            _listadoDeReportes = listadoDeReportes;
            _eliminarReporte = eliminarReporte;
            _eliminarProducto = eliminarProducto;
            _obtenerReporte = obtenerReporte;
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
        public IActionResult AceptarReporte(int id)
        {

            var reporte = _obtenerReporte.Ejecutar(id);

            _eliminarProducto.Ejecutar(reporte.producto.id);
            //_eliminarReporte.Ejecutar(id);
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
