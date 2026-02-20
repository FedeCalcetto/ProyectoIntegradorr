using ProyectoIntegrador.LogicaNegocio.Entidades;

namespace ProyectoIntegrador_Web.Models
{
    public class FacturaNoFiscalClienteViewModel
    {
        public int FacturaId { get; set; }
        public int ProductoId { get; set; }
        public string NombreCliente { get; set; }
        public string NombreProducto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
        public decimal? MiCalificacion { get; set; }
        public List<LineaFactura> ItemsFactura { get; set; }

    }
}
