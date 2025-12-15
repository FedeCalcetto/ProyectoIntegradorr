using ProyectoIntegrador.LogicaNegocio.Entidades;

namespace ProyectoIntegrador_Web.Models
{
    public class ProductosFiltradosViewModel
    {

        public List<Producto> Productos { get; set; } = new List<Producto>();
        public string Filtro { get; set; }
        public int? PrecioMin { get; set; }
        public int? PrecioMax { get; set; }

        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
        public int TamanoPagina { get; set; }

        public bool TienePaginaAnterior => PaginaActual > 1;
        public bool TienePaginaSiguiente => PaginaActual < TotalPaginas;
    }
}
