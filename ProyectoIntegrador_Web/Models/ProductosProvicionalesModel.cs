using ProyectoIntegrador.LogicaNegocio.Entidades;

namespace ProyectoIntegrador_Web.Models
{
    public class ProductosProvicionalesModel
    {
        public int idProducto { get; set; }
        public IEnumerable<Producto> Productos { get; set; }
    }
}
