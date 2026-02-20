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
        Task<Calificación?> ObtenerPorUsuarioYProducto(int usuarioId, int productoId);

        void Agregar(int productoId, int usuarioId, decimal puntaje);

        void Actualizar(Calificación calificacion);

        decimal ObtenerPromedioPorProducto(int productoId);
        int  ObtenerTotalCalificaciones(int productoId);
    }
}
