using ProyectoIntegrador.LogicaNegocio.Entidades;

namespace ProyectoIntegrador_Web.Models
{
    public class PedidosPersonalizadosPaginadosViewModel
    {
        public List<PedidoPersonalizado> Pedidos { get; set; } = new();

        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
        public string Busqueda { get; set; }
        public string Orden { get; set; }
    }
}
