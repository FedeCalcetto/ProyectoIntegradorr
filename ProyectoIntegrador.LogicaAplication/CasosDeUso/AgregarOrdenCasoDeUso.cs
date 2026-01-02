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
            var itemsCarrito = _carritoRepo.ObtenerItemsDeUsuario(usuarioId);

            var orden = new Orden(usuarioId);

            foreach (var item in itemsCarrito)
            {
                orden.AgregarItem(
                    item.productoId,
                    item.producto.nombre,
                    item.cantidad,
                    item.producto.precio
                );
            }

            orden.CalcularTotal();

            await _ordenRepo.CrearOrdenAsync(orden);

            return orden.Id;
        }
    }

}
