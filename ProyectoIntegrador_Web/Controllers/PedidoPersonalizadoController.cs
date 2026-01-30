using Microsoft.AspNetCore.Mvc;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador_Web.Models;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using ProyectoIntegrador.LogicaAplication.Dtos;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Excepciones;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador_Web.Services;

namespace ProyectoIntegrador_Web.Controllers
{
    public class PedidoPersonalizadoController : Controller 
    {
        private readonly ICrearPedidoPersonalizado _crearPedido;
        private readonly IObtenerPedidosPendientes _obtenerPendientes;
        private readonly IAceptarPedidoPersonalizado _aceptarPedido;
        private readonly IObtenerMisEncargos _obtenerMisEncargos;
        private readonly IFinalizarPedidoPersonalizado _finalizarPedido;

        public PedidoPersonalizadoController(
            ICrearPedidoPersonalizado crearPedido,
            IObtenerPedidosPendientes obtenerPendientes,
            IAceptarPedidoPersonalizado aceptarPedido,
            IObtenerMisEncargos obtenerMisEncargos,
            IFinalizarPedidoPersonalizado finalizarPedido)
        {
            _crearPedido = crearPedido;
            _obtenerPendientes = obtenerPendientes;
            _aceptarPedido = aceptarPedido;
            _obtenerMisEncargos = obtenerMisEncargos;
            _finalizarPedido = finalizarPedido;
        }

        //  ARTESANO – ver pedidos disponibles
        public IActionResult PedidosPersonalizados(int pagina = 1,string busqueda = "",string orden = "fecha_desc")
        {
            var pedidos = _obtenerPendientes.Ejecutar(busqueda, orden);

            const int pageSize = 5;

            var total = pedidos.Count();
            var pedidosPagina = pedidos
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var vm = new PedidosPersonalizadosPaginadosViewModel
            {
                Pedidos = pedidosPagina,
                PaginaActual = pagina,
                TotalPaginas = (int)Math.Ceiling(total / (double)pageSize),
                Busqueda = busqueda,
                Orden = orden
            };

            return View(vm);
        }


        //  ARTESANO – aceptar pedido
        [HttpPost]
        public async Task<IActionResult> Aceptar(int id)
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Usuario");

            try
            {
                await _aceptarPedido.Ejecutar(id, email);
                TempData["Mensaje"] = "Pedido aceptado correctamente.";
                
            }
            catch (PedidoYaAceptadoException ex)
            {
                TempData["Mensaje"] = ex.Message;
                TempData["TipoMensaje"] = "danger";
            }
            catch (Exception)
            {
                TempData["Mensaje"] = "Ocurrió un error al aceptar el pedido.";
                TempData["TipoMensaje"] = "danger";
            }

            return RedirectToAction(nameof(PedidosPersonalizados));
        }

        //  ARTESANO – ver mis encargos
        public IActionResult MisEncargos(int pagina = 1)
        {
            var email = HttpContext.Session.GetString("loginUsuario");

            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Usuario");

            const int pageSize = 5; // cuantos por página

            var pedidos = _obtenerMisEncargos.Ejecutar(email).ToList();

            var total = pedidos.Count;

            var pedidosPagina = pedidos
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var vm = new MisEncargosPaginadosViewModel
            {
                Pedidos = pedidosPagina,
                PaginaActual = pagina,
                TotalPaginas = (int)Math.Ceiling(total / (double)pageSize)
            };

            return View(vm);
        }

        //  ARTESANO – finalizar pedido
        [HttpPost]
        public async Task<IActionResult> Finalizar(int id)
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Usuario");

            await _finalizarPedido.Ejecutar(id);
            return RedirectToAction(nameof(MisEncargos));
        }

        public IActionResult Crear()
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Usuario");

            return View();
        }

        [HttpPost]
        public IActionResult Crear(CrearPedidoPersonalizadoViewModel vm)
        {
            var email = HttpContext.Session.GetString("loginUsuario");
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Usuario");

            if (!ModelState.IsValid)
                return View(vm);

            var dto = new CrearPedidoDto
            {
                Titulo = vm.Titulo,
                Descripcion = vm.Descripcion,
                EmailCliente = email
            };

            _crearPedido.Ejecutar(dto);

            TempData["Mensaje"] = "Tu pedido fue enviado correctamente.";

            return RedirectToAction("Catalogo", "Usuario");
        }
    }
}