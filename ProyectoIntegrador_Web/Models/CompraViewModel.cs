using ProyectoIntegrador.LogicaNegocio.Entidades;

namespace ProyectoIntegrador_Web.Models
{
    public class CompraViewModel
    {
        public List<FacturaNoFiscalCliente> Compras { get; set; } = new();
        public string Compra { get; set; }

        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
    }
}
