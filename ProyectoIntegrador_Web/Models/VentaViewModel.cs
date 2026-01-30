using ProyectoIntegrador.LogicaNegocio.Entidades;

namespace ProyectoIntegrador_Web.Models
{
    public class VentaViewModel
    {
        public List<FacturaNoFiscalArtesano> Ventas { get; set; } = new();
        public string Venta { get; set; }

        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
    }
}
