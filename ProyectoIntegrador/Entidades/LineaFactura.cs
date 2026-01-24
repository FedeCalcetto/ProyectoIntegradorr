using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
 //   [PrimaryKey(nameof(idProducto), [nameof(idFactura)])]
    public class LineaFactura
    {
        public int Id { get; set; }
        public int? idProducto { get; set; } //Es una referencia histórica
        public string NombreProducto { get; set; }
        public int idFactura { get; set; }
        //public FacturaNoFiscal factura { get; set; }
        public int artesanoId { get; set; } // solo referencia
        public string NombreArtesano { get; set; }
        public int precioUnitario { get; set; }
        public int cantidad { get; set; }

    }
}
