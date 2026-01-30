using ProyectoIntegrador.LogicaAplication.Dtos;
using ProyectoIntegrador.LogicaNegocio.Entidades;

namespace ProyectoIntegrador_Web.Models
{
    public class DashboardViewModel
    {
        public int CantidadVentasTotal { get; set; } 
        public int CantidadVentasMesActual { get; set; }
        public int CantidadVentasAnoActual { get; set; }
        public double VariacionVentasMensual { get; set; }
        public List<TopVentasDTO> TopProductosVentas { get; set; } = new();
        public List<VentasPorMesDto> GraficoVentas { get; set; } = new();
    }
}
