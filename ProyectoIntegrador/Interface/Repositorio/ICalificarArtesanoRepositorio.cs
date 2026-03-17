using ProyectoIntegrador.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Interface.Repositorio
{
    public interface ICalificarArtesanoRepositorio
    {
        void Agregar(int artesanoId, int usuarioId, decimal puntaje);

        void Actualizar(CalificacionArtesano calificacion);

        Task<CalificacionArtesano?> ObtenerPorUsuarioYArtesano(int usuarioId, int artesanoId);

        decimal ObtenerPromedioPorArtesano(int artesanoId);

        int ObtenerTotalCalificaciones(int artesanoId);

        void EliminarParaArtesano(int artesanoId);
    }
}
