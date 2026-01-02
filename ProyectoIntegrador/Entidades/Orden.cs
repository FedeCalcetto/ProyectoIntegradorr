using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class Orden
    {
        public Guid Id { get; set; }

        public int UsuarioId { get; set; }

        public DateTime FechaCreacion { get; set; } 

        public EstadoOrden Estado { get; set; } 

        public decimal Total { get; set; }

        // Mercado Pago
        public string? PreferenceId { get; set; }

        // Navegación
        public ICollection<OrdenItem> Items { get; set; } = new List<OrdenItem>();

        private Orden() { }
        public Orden(int usuarioId)
        {
            Id = Guid.NewGuid();
            UsuarioId = usuarioId;
            FechaCreacion = DateTime.UtcNow;
            Estado = EstadoOrden.PendientePago;
        }

        public void AgregarItem(int productoId, string nombre, int cantidad, decimal precio)
        {
            Items.Add(new OrdenItem
            {
                ProductoId = productoId,
                NombreProducto = nombre,
                Cantidad = cantidad,
                PrecioUnitario = precio
            });
        }

        public void CalcularTotal()
        {
            Total = Items.Sum(i => i.Cantidad * i.PrecioUnitario);
        }

        public void AsignarPreferenceId(string preferenceId)
        {
            PreferenceId = preferenceId;
        }
        public void MarcarComoPagada()
        {
            if (Estado != EstadoOrden.PendientePago)
                throw new InvalidOperationException("Solo se puede pagar una orden pendiente");

            Estado = EstadoOrden.Pagada;
        }

        public void MarcarComoCancelada()
        {
            if (Estado != EstadoOrden.PendientePago)
                throw new InvalidOperationException("Solo se puede cancelar una orden pendiente");

            Estado = EstadoOrden.Cancelada;
        }
    }


}
