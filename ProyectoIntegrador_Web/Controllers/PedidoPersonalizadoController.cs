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
        public IActionResult PedidosPersonalizados()
        {
            var pedidos = _obtenerPendientes.Ejecutar();
            return View(pedidos);
        }

        //  ARTESANO – aceptar pedido
        [HttpPost]
        public IActionResult Aceptar(int id)
        {
            _aceptarPedido.Ejecutar(id, HttpContext.Session.GetString("loginUsuario")); 
            return RedirectToAction(nameof(PedidosPersonalizados));
        }

        //  ARTESANO – ver mis encargos
        public IActionResult MisEncargos()
        {
            var pedidos = _obtenerMisEncargos.Ejecutar(HttpContext.Session.GetString("loginUsuario"));
            return View(pedidos);
        }

        //  ARTESANO – finalizar pedido
        [HttpPost]
        public IActionResult Finalizar(int id)
        {
            _finalizarPedido.Ejecutar(id);
            return RedirectToAction(nameof(MisEncargos));
        }
    }
}