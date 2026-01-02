using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class EliminarReporteCasoDeUso : IEliminarReporte
    {
        public readonly IReporteRepositorio _reporteRepositorio;

        public EliminarReporteCasoDeUso(IReporteRepositorio reporteRepositorio)
        {
            _reporteRepositorio = reporteRepositorio;
        }

        public void Ejecutar(int id)
        {
            _reporteRepositorio.Eliminar(id);
        }
    }
}
