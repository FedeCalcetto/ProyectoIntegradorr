using ProyectoIntegrador.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Interface.Repositorio
{
    public interface ICalificarProductoRepositorio
    {
        void Agregar(int productoId, int usuarioId, decimal puntaje);

        void Actualizar(CalificacionProducto calificacion);

        Task<CalificacionProducto?> ObtenerPorUsuarioYProducto(int usuarioId, int productoId);

        decimal ObtenerPromedioPorProducto(int productoId);

        int ObtenerTotalCalificaciones(int productoId);

        void EliminarParaProducto(int productoId);
    }
}
