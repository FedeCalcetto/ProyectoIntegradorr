using MercadoPago.Resource.Payment;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;

namespace ProyectoIntegrador_Web.Services
{
    public class ProcesarOrdenService : IProcesarOrdenService
    {
        private readonly IOrdenRepositorio _ordenRepo;
        private readonly IFacturaRepositorio _facturaRepo;
        private readonly IProductoRepositorio _productoRepo;

        public ProcesarOrdenService(
            IOrdenRepositorio ordenRepo, IFacturaRepositorio facturaRepo, IProductoRepositorio productoRepo)
        {
            _ordenRepo = ordenRepo;
            _facturaRepo = facturaRepo;
            _productoRepo = productoRepo;
        }

        public async Task ProcesarOrdenCanceladaAsync(Orden orden)
        {
            orden.MarcarComoCancelada();
            await _ordenRepo.ActualizarOrdenAsync(orden);
        }

        public async Task ProcesarOrdenPagadaAsync(Orden orden, long paymentId)
        {
            if (orden.Estado != EstadoOrden.Pagada)
            {
                orden.MarcarComoPagada(paymentId);

                var ordenItems = orden.Items;

                _facturaRepo.CrearFacturas(orden);
                foreach (var item in ordenItems)
                {
                    var producto = _productoRepo
                        .Obtener(item.ProductoId);

                    producto.DescontarStock(item.Cantidad);

                    _productoRepo.Editar(producto);
                }

                await _ordenRepo.ActualizarOrdenAsync(orden);
            }
        }

        public async Task ProcesarOrdenPendienteAsync(Orden orden)
        {
            orden.MarcarComoPendiente();
            await _ordenRepo.ActualizarOrdenAsync(orden);
        }

        public async Task ProcesarOrdenRechazadaAsync(Orden orden)
        {
            orden.MarcarComoRechazada();
            await _ordenRepo.ActualizarOrdenAsync(orden);
        }
    }
}
