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
    public class ObtenerReporteCasoeUso : IObtenerReporte
    {

        private readonly IReporteRepositorio _reporteRepositorio;

        public ObtenerReporteCasoeUso(IReporteRepositorio reporteRepositorio)
        {
            _reporteRepositorio = reporteRepositorio;
        }

        public Reporte Ejecutar(int id)
        {
            return _reporteRepositorio.Obtener(id);
        }
    }
}
