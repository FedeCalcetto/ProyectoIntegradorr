using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class AgregarOrdenCasoDeUso : IAgregarOrden
    {
        private readonly IOrdenRepositorio _ordenRepo;
        private readonly ICarritoRepositorio _carritoRepo;

        public AgregarOrdenCasoDeUso(
            IOrdenRepositorio ordenRepo,
            ICarritoRepositorio carritoRepo)
        {
            _ordenRepo = ordenRepo;
            _carritoRepo = carritoRepo;
        }

        public async Task<Guid> AgregarOrdenAsync(int usuarioId)
        {
            var carrito =  _carritoRepo.ObtenerCarritoDeUsuario(usuarioId);

            if (carrito == null || !carrito.Items.Any())
                throw new ApplicationException("El carrito está vacío");

            var orden = new Orden(usuarioId);
            foreach (var item in carrito.Items)
            {
                orden.AgregarItem(
                        item.productoId,
                        item.producto.nombre,
                        item.cantidad,
                        item.producto.precio,
                        item.producto.ArtesanoId
                );
            }
            orden.CalcularTotal();
            await _ordenRepo.CrearOrdenAsync(orden);
            return orden.Id;
        }

        public async Task<List<Guid>> AgregarOrdenesAsync(int usuarioId)
        {
            var itemsCarrito = _carritoRepo.ObtenerItemsDeUsuario(usuarioId);

            // Agrupar por artesano
            var itemsPorArtesano = itemsCarrito
                .GroupBy(i => i.producto.ArtesanoId);

            var ordenesIds = new List<Guid>();

            foreach (var grupo in itemsPorArtesano)
            {
                var orden = new Orden(usuarioId);

                foreach (var item in grupo)
                {
                    orden.AgregarItem(
                        item.productoId,
                        item.producto.nombre,
                        item.cantidad,
                        item.producto.precio,
                        item.producto.ArtesanoId

                    );
                }

                orden.CalcularTotal();

                await _ordenRepo.CrearOrdenAsync(orden);

                ordenesIds.Add(orden.Id);
            }

            return ordenesIds;
        }

      
    }

}
