using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;

namespace ProyectoIntegrador_Web.Services
{
    public class CarritoService : ICarritoService
    {
        private readonly IMostrarProductosCarrito _mostrarProductosCarrito;
        private readonly ICarritoRepositorio _carritoRepo;

        public CarritoService(IMostrarProductosCarrito mostrarProductosCarrito, ICarritoRepositorio carritoRepositorio)
        {
            _mostrarProductosCarrito = mostrarProductosCarrito;
            _carritoRepo = carritoRepositorio;
        }

        public async Task LimpiarCarritoAsync(int usuarioId)
        {
            var itemsCarrito = _mostrarProductosCarrito.mostrarProductos(usuarioId);
            itemsCarrito.Clear();
            await _carritoRepo.EliminarItemsCarrito(usuarioId);
            await Task.CompletedTask;
        }
    }
}
