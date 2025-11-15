using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class Artesano : Usuario
    {
        public string descripcion { get; set; }
        [StringLength(9, MinimumLength = 9, ErrorMessage = "numero invalido")]
        public string telefono { get; set; }
        public List<Factura> ventas { get; set; } = new List<Factura>();
        public List<Producto> productos { get; set; } = new List<Producto>();
        public string? foto { get; set; }
        //public List<PedidoPersonalizado> pedidosArtesano { get; set; }
    }
}
