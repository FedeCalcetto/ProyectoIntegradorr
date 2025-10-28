using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class Factura
    {
        public int Id { get; set; }
        public List<LineaFactura> itemsFactura { get; set; }
        public int Total { get; set; }
        public DateTime Fecha { get; set; }
        public Cliente Cliente { get; set; }
    }
}
