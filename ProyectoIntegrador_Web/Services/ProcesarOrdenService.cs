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
            //// 🔁 Idempotencia: si este pago ya fue procesado, no hacer nada
            //if (orden.PagosAprobados.Contains(paymentId))
            //    return;

            //// 1️⃣ Marcar este pago como aprobado
            //orden.MarcarPagoAprobado(paymentId);

            //// 2️⃣ Si TODAVÍA no están todos aprobados → solo guardar y salir
            //if (!orden.TodosLosPagosAprobados())
            //{
            //    orden.Estado = EstadoOrden.PagoParcial;
            //    await _ordenRepo.ActualizarOrdenAsync(orden);
            //    return;
            //}

            //// 3️⃣ Si ya estaba pagada, no repetir lógica irreversible
            //if (orden.Estado == EstadoOrden.Pagada)
            //    return;

            //// 4️⃣ AHORA SÍ: la orden se paga DEFINITIVAMENTE
            //orden.MarcarComoPagada(); // ⬅️ OJO: ya NO recibe paymentId

            //// 5️⃣ Crear facturas (una sola vez)
            //_facturaRepo.CrearFacturas(orden);

            //// 6️⃣ Descontar stock (una sola vez)
            //foreach (var item in orden.Items)
            //{
            //    var producto = _productoRepo.Obtener(item.ProductoId);
            //    producto.DescontarStock(item.Cantidad);
            //    _productoRepo.Editar(producto);
            //}

            //// 7️⃣ Guardar todo
            //await _ordenRepo.ActualizarOrdenAsync(orden);

            if (orden.Estado != EstadoOrden.Pagada)
            {
                //orden.MarcarPagoAprobado(paymentId);
                //if (orden.TodosLosPagosAprobados())
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
