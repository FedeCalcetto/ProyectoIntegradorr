using ProyectoIntegrador.LogicaNegocio.Entidades;

namespace ProyectoIntegrador_Web.Models
{
    public class ProductosDelArtesanoViewModel
    {
        public Artesano Artesano { get; set; }
        public IEnumerable<Producto> Productos { get; set; }
    }
}
