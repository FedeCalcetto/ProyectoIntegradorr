using ProyectoIntegrador.LogicaAplication.Dtos;
using ProyectoIntegrador.LogicaNegocio.Entidades;

namespace ProyectoIntegrador_Web.Models
{
    public class DetallesProductoViewModel
    {
        public int Id { get; set; }  
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? Precio { get; set; }
        public int? Stock { get; set; }
        public string? Imagen { get; set; }
        public List<ProductoFoto>? Fotos { get; set; } = new();
        public string? Artesano { get; set; }
        public int? ArtesanoId { get; set; }
        public string? SubCategoria { get; set; }
        public AgregarReporteDto Reporte { get; set; } = new();
        public bool EsFavorito { get; set; }
    }
}
