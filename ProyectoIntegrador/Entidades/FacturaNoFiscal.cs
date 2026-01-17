using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public abstract class FacturaNoFiscal
    {
        public int Id { get; set; }
        public List<LineaFactura> itemsFactura { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public Guid OrdenId { get; set; }
        public Orden Orden { get; set; }
    }
}
