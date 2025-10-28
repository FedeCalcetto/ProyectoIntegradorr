using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class Artesano : Usuario
    {
        public int id { get; set; }
        public string foto { get; set; }
        public string descripcion { get; set; }
        public string telefono { get; set; }
        public List<Factura> ventas { get; set; } = new List<Factura>();
        public List<Producto> productos { get; set; } = new List<Producto>();
    }
}
