using ProyectoIntegrador.LogicaAplication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Interface
{
    public interface IDashboard
    {
        public DashboardDto Ejecutar(string emailArtesano, int cantidadMeses);
    }
}
