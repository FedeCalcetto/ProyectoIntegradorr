using ProyectoIntegrador.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Interface.Repositorio
{
    public interface ICarritoRepositorio
    {
        Carrito ObtenerCarritoDeUsuario(int usuarioId);
        List<CarritoItem> ObtenerItemsDeUsuario(int usuarioId);
        CarritoItem BuscarProducto(int productoId, int idUsuario);
        void AgregarItem(int usuarioId, int productoId, int cantidad);
        void EliminarItem(int itemId, int cantidadABorrar);
        void Guardar();
    }
}
