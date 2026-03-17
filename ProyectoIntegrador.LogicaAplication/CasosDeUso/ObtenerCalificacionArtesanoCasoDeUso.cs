using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoIntegrador.LogicaAplication.Interface;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class ObtenerCalificacionArtesanoCasoDeUso : IObtenerPromedioCalificacionArtesano
    {
        private readonly ICalificarArtesanoRepositorio _calificarRepo;

        public ObtenerCalificacionArtesanoCasoDeUso(ICalificarArtesanoRepositorio calificarRepo)
        {
            _calificarRepo = calificarRepo;
        }
        public decimal ObtenerPromedioPorArtesano(int arteId)
        {
            return _calificarRepo.ObtenerPromedioPorArtesano(arteId);
        }

        public int ObtenerTotalCalificacionesArtesano(int arteId)
        {
            return _calificarRepo.ObtenerTotalCalificaciones(arteId);
        }
    }
}
