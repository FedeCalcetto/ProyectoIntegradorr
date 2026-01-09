using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class ArtesanoConReportesCasoDeUso : IArtesanoConReportes
    {

        private readonly IReporteRepositorio _reporteRepositorio;

        public ArtesanoConReportesCasoDeUso(IReporteRepositorio reporteRepositorio)
        {
            _reporteRepositorio = reporteRepositorio;
        }

        public bool Ejecutar(int artesanoId)
        {
            return _reporteRepositorio.ArtesanoConReportes(artesanoId);
        }
    }
}
