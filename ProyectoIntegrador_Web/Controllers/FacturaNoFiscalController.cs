using Microsoft.AspNetCore.Mvc;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador_Web.Models;
using ProyectoIntegrador_Web.Services;

namespace ProyectoIntegrador_Web.Controllers
{
    public class FacturaNoFiscalController : Controller
    {
        private readonly IObtenerFacturaClientePorOrden _obtenerFacturaClientePorOrden;
        private readonly IObtenerFacturasDeUnCliente _obtenerFacturasDeUnCliente;
        private readonly IObtenerFacturaArtesano _obtenerFacturaArtesano;
        private readonly IObtenerFacturaCliente _obtenerFacturCliente;
        private readonly IObtenerUsuario _obtenerUsuario;
        private readonly PdfClienteService _pdf;

        public FacturaNoFiscalController(PdfClienteService pdf, IObtenerFacturaClientePorOrden obtenerFacturaClientePorOrden, 
            IObtenerFacturasDeUnCliente obtenerFacturasDeUnCliente, IObtenerFacturaArtesano obtenerFacturaArtesano, IObtenerFacturaCliente obtenerFacturCliente, IObtenerUsuario obtenerUsuario)
        {
            _pdf = pdf;
            _obtenerFacturaClientePorOrden = obtenerFacturaClientePorOrden;
            _obtenerFacturasDeUnCliente = obtenerFacturasDeUnCliente;
            _obtenerFacturaArtesano = obtenerFacturaArtesano;
            _obtenerFacturaArtesano = obtenerFacturaArtesano;
            _obtenerFacturCliente = obtenerFacturCliente;
            _obtenerUsuario = obtenerUsuario;
        }
        public IActionResult FacturaPorOrden(Guid ordenId)
        {
            var factura = _obtenerFacturaClientePorOrden.obtenerFacturaClientePorOrden(ordenId);
            return View("ClienteFacturaNoFiscal", factura);
        }

        public IActionResult FacturasDelCliente()
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            if (email == null)
                return NotFound();

            var facturas = _obtenerFacturasDeUnCliente.obtenerFacturasDeUnCliente(email);
            return View(facturas);
        }

        public IActionResult FacturasArtesano(int pagina = 1)
        {
            var rol = HttpContext.Session.GetString("Rol");
            var email = HttpContext.Session.GetString("loginUsuario");

            if (string.IsNullOrEmpty(rol) || rol.Trim().ToUpper() != "ARTESANO")
            {
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Index", "Home");
            }

            var usuario = _obtenerUsuario.Ejecutar(email);

            var facturas = _obtenerFacturaArtesano
                .obtenerTodasLasFacturasDelArtesano(usuario.id);

            const int pageSize = 5;
            var total = facturas.Count();

            var pedidosPagina = facturas
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var vm = new VentaViewModel
            {
                Ventas = pedidosPagina,
                PaginaActual = pagina,
                TotalPaginas = (int)Math.Ceiling(total / (double)pageSize),
            };

            return View(vm);
        }


        public IActionResult ArtesanoFacturaNoFiscal(int facturaId)
        {
            var factura = _obtenerFacturaArtesano.obtenerFacturaArteasano(facturaId);
            return View("ArtesanoFacturaNoFiscal", factura);
        }

        public IActionResult PDFCliente(int id)
        {
            var factura = _obtenerFacturCliente.obtenerFacturaCliente(id);

            if (factura == null)
                return NotFound();
            var pdf = _pdf.Generar(factura);

            return File(pdf, "application/pdf", $"Factura_{factura.Id}.pdf");
        }

       
    }
}
