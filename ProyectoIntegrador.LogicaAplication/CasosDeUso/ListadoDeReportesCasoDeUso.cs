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
    public class ListadoDeReportesCasoDeUso : IListadoDeReportes
    {
        private readonly IReporteRepositorio _reporteRepositorio;

        public ListadoDeReportesCasoDeUso(IReporteRepositorio reporteRepositorio)
        {
            _reporteRepositorio = reporteRepositorio;
        }
        public IEnumerable<Reporte> Ejecutar()
        {
            return _reporteRepositorio.ObtenerTodos();
        }
    }
}
