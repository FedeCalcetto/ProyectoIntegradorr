using ProyectoIntegrador.LogicaNegocio.Entidades;

namespace ProyectoIntegrador_Web.Models
{
    public class CarritoViewModel
    {
        public List<CarritoItem> ItemsCarrito { get; set; }
        public List<Producto> ProductosDeInteres { get; set; }
    }
}
