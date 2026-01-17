using Microsoft.AspNetCore.Mvc;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using Rotativa.AspNetCore;

namespace ProyectoIntegrador_Web.Controllers
{
    public class FacturaNoFiscalController : Controller
    {
        private readonly IFacturaRepositorio _facturaRepo;
        private readonly IObtenerUsuario _obtenerUsuario;

        public FacturaNoFiscalController(IFacturaRepositorio facturaRepo,IObtenerUsuario obtenerUsuario)
        {
            _facturaRepo = facturaRepo;
           _obtenerUsuario = obtenerUsuario;

        }
        public IActionResult FacturaPorOrden(Guid ordenId)
        {
            var factura = _facturaRepo.ObtenerFacturaClientePorOrden(ordenId);
            return View("ClienteFacturaNoFiscal", factura);
        }

        public IActionResult FacturasDelCliente()
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            var facturas = _facturaRepo.ObtenerPorCliente(email);
            return View(facturas);
        }

        public IActionResult FacturasArtesano()
        {
            return RedirectToAction("Inicio", "Artesano");
        }

        public IActionResult ArtesanoFacturaNoFiscal(int facturaId)
        {
            var factura = _facturaRepo.Obtener(facturaId);
            return View("ArtesanoFacturaNoFiscal", factura);
        }

        public IActionResult PDFCliente(int id)
        {
            var factura = _facturaRepo.ObtenerFacturaCliente(id);

            if (factura == null)
                return NotFound();

            return new ViewAsPdf("PDFCliente", factura)
            {
                FileName = $"Factura_{factura.Id}.pdf"
            };
        }
    }
}
