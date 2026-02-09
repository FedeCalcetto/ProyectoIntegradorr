using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador_Web.Models;
using ProyectoIntegrador_Web.Services;

namespace ProyectoIntegrador_Web.Controllers
{
    public class CarritoController : Controller
    {
        private readonly IObtenerProducto _obtenerProducto;
        private readonly IMostrarProductosCarrito _mostrarProductosCarrito;
        private readonly IObtenerUsuario _obtenerUsuario;
        private readonly IAgragarAlCarrito _agragarAlCarrito;
        private readonly IEliminarItemDelCarrito _eliminarItem;
        private readonly IObtenerProductosDeInteres _productosInteres;
        private readonly IAgregarOrden _agregarOrden;
        private readonly ICarritoService _carritoService;

        public CarritoController(IObtenerProducto obtenerProducto, IMostrarProductosCarrito mostrarProductosCarrito, IObtenerUsuario obtenerUsuario, 
            IAgragarAlCarrito agragarAlCarrito, IEliminarItemDelCarrito eliminarItem, IObtenerProductosDeInteres productosInteres, IAgregarOrden agregarOrden, ICarritoService carritoService)
        {
            _obtenerProducto = obtenerProducto;
            _mostrarProductosCarrito = mostrarProductosCarrito;
            _obtenerUsuario = obtenerUsuario;
            _agragarAlCarrito = agragarAlCarrito;
            _eliminarItem = eliminarItem;
            _productosInteres = productosInteres;
            _agregarOrden = agregarOrden;
            _carritoService = carritoService;
        }

        public IActionResult Index()
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            var usuario = _obtenerUsuario.Ejecutar(email);
            var itemsCarrito = _mostrarProductosCarrito.mostrarProductos(usuario.id);
            var prodInteres = _productosInteres.Obtener(usuario.id);

            var vm = new CarritoViewModel
            {
                ItemsCarrito = itemsCarrito,
                ProductosDeInteres = prodInteres
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult AgregarAlCarrito(int productoId, int cantidad)
        {
            var producto = _obtenerProducto.obtener(productoId);
            var email = HttpContext.Session.GetString("loginUsuario");
            var usuario = _obtenerUsuario.Ejecutar(email);
            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }
            if (HttpContext.Session.GetString("Rol") == null)
            {
                TempData["Error"] =
                    "Debe iniciar sesión para agregar un producto al carrito.";

                return RedirectToAction("DetallesProducto", "Producto", new { id = productoId });
            }
            
            _agragarAlCarrito.agregarAlCarrito(productoId, usuario.id, cantidad);
            var itemsCarrito = _mostrarProductosCarrito.mostrarProductos(usuario.id);

            ViewBag.CantidadProductos = itemsCarrito.Sum(i => i.cantidad);

            return RedirectToAction("Catalogo", "Usuario");
        }



        [HttpPost]
        public IActionResult EliminarItemDelCarrito(int idItemCarrito, int cantidadABorrar)
        {
            _eliminarItem.Eliminar(idItemCarrito, cantidadABorrar);
            return RedirectToAction("Index", "Carrito");
        }

        [HttpPost]
        public async Task<IActionResult> ContinuarCompra()
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            var usuario = _obtenerUsuario.Ejecutar(email);

            // Crear UNA sola orden con todos los ítems del carrito
            var ordenId = await _agregarOrden.AgregarOrdenAsync(usuario.id);

            // Ir directo a pagar
            return RedirectToAction(
                "CrearPago",
                "Pago",
                new {ordenId}
            );
        }

        //[HttpPost]
        //public async Task<IActionResult> ContinuarCompra()
        //{
        //    var email = HttpContext.Session.GetString("loginUsuario");
        //    var usuario = _obtenerUsuario.Ejecutar(email);

        //    // Crear UNA sola orden con todos los ítems del carrito
        //    var ordenId = await _agregarOrden.AgregarOrdenAsync(usuario.id);

        //    // Ir directo a pagar
        //    return RedirectToAction(
        //     "Checkout",
        //     "PagoNoApi",
        //     new { ordenId }
        // );
        //}

    }
}
