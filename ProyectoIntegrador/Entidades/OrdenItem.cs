using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class OrdenItem
    {
        public int Id { get; set; }

        public Guid OrdenId { get; set; }
        public Orden Orden { get; set; } = null!;

        public int ProductoId { get; set; }

        public string NombreProducto { get; set; } = string.Empty;

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
