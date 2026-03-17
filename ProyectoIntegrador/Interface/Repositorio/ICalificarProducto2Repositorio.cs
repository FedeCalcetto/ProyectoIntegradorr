using ProyectoIntegrador.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Interface.Repositorio
{
    public interface ICalificarProducto2Repositorio
    {
        Task<Calificación?> ObtenerPorUsuarioYProducto(int usuarioId, int productoId);

        Task<Calificación?> ObtenerPorUsuarioYArtesano(int usuarioId, int arteId);

        void Agregar(int productoId, int usuarioId, decimal puntaje);

        void AgregarParaArtesano(int arteId, int usuarioId, decimal puntaje);

        void Actualizar(Calificación calificacion);

        decimal ObtenerPromedioPorProducto(int productoId);

        decimal ObtenerPromedioPorArtesano(int arteId);

        int  ObtenerTotalCalificaciones(int productoId);

        int ObtenerTotalCalificacionesArtesano(int arteId);


    }
}
