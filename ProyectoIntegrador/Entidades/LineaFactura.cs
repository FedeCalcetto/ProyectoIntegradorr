using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    [PrimaryKey(nameof(idProducto), [nameof(idFactura)])]
    public class LineaFactura
    {

        public int idProducto { get; set; }
        public int idFactura { get; set; }
        public Producto producto { get; set; }
        public Factura factura { get; set; }
        public int precioUnitario { get; set; }
        public int cantidad { get; set; }

    }
}
