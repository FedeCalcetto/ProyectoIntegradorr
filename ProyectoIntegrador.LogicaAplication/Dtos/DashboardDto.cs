using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Dtos
{
    public class DashboardDto
    {
        public int CantidadVentasTotal { get; set; }
        public int CantidadVentasMesActual { get; set; }
        public int CantidadVentasAnoActual { get; set; }
        public double VariacionVentasMensual { get; set; }

        public List<VentasPorMesDto> GraficoVentas { get; set; } = new();
        public List<TopVentasDTO> TopProductosVentas { get; set; } = new();
    }
}
