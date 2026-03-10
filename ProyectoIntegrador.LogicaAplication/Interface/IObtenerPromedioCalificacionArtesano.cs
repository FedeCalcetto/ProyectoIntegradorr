using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Interface
{
    public interface IObtenerPromedioCalificacionArtesano
    {
        decimal ObtenerPromedioPorArtesano(int arteId);
        int ObtenerTotalCalificacionesArtesano(int arteId);
    }
}
