using Microsoft.EntityFrameworkCore;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Excepciones;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.EntityFrameWork.Repositorios
{
    public class CarritoEFRepositorio : ICarritoRepositorio
    {
        private readonly ProyectoDBContext _contexto;

        public CarritoEFRepositorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }

        public void AgregarItem(int usuarioId, int productoId, int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero");

            var carrito = _contexto.Carritos
                .Include(c => c.Items)
                .FirstOrDefault(c => c.UsuarioId == usuarioId);

            if (carrito == null)
            {
                carrito = new Carrito
                {
                    UsuarioId = usuarioId,
                    Items = new List<CarritoItem>()
                };

                _contexto.Carritos.Add(carrito);
                _contexto.SaveChanges();
            }

            var itemExistente = carrito.Items
                .FirstOrDefault(i => i.productoId == productoId);

            if (itemExistente != null)
            {
                itemExistente.SumarCantidad(cantidad);
            }
            else
            {
                var nuevoItem = new CarritoItem(
                    carrito.Id,
                    productoId,
                    cantidad
                );

                carrito.Items.Add(nuevoItem);
            }

            _contexto.SaveChanges();
        }

        public void EliminarItem(int itemId, int cantidadABorrar)
        {
            var carritoItem = _contexto.CarritoItems.Find(itemId);
            carritoItem.RestarCantidad(cantidadABorrar);
            if (carritoItem.QuedaEnCero())
            {
                _contexto.CarritoItems.Remove(carritoItem);
            }

            _contexto.SaveChanges();
        }

        public void Guardar()
        {
            _contexto.SaveChanges();
                }

        public Carrito ObtenerCarritoDeUsuario(int usuarioId)
        {
            return _contexto.Carritos
                       .Include(c => c.Items)
                       .ThenInclude(i => i.producto)
                       .FirstOrDefault(c => c.UsuarioId == usuarioId);
        }

        public List<CarritoItem> ObtenerItemsDeUsuario(int usuarioId)
        {
            return _contexto.CarritoItems
           .Include(ci => ci.producto)
           .Where(ci => ci.carrito.UsuarioId == usuarioId)
           .ToList();
        }

        public CarritoItem BuscarProducto(int productoId, int idUsuario) {

            return _contexto.CarritoItems
        .Include(ci => ci.carrito)
        .FirstOrDefault(ci =>
            ci.productoId == productoId &&
            ci.carrito.UsuarioId == idUsuario
        );

        }

        public async Task EliminarItemsCarrito(int usuarioId)
        {
            await _contexto.CarritoItems
                .Where(ci => ci.carrito.UsuarioId == usuarioId)
                .ExecuteDeleteAsync();
                _contexto.SaveChanges();
        }

        public bool ExisteEnCarrito(int id)
        {
            return _contexto.CarritoItems
                .Any(cI => cI.productoId == id);
        }
    }
}
