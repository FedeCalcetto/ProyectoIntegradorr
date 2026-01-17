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
        public int ClienteId { get; private set; }
        public Cliente Cliente { get; private set; }

        //Sirve para validar el pago correspondiente al artesano
       // public string? MercadoPagoSellerUserId { get; private set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaPago { get; private set; }
        public EstadoOrden Estado { get; set; } 
        public decimal Total { get; set; }
        // Mercado Pago
        public string? PreferenceId { get; set; }
        //public long? MercadoPagoPaymentId { get; private set; }
        // Navegación
        public ICollection<OrdenItem> Items { get; set; } = new List<OrdenItem>();

        private Orden() { }
        public Orden(int usuarioId)
        {
            Id = Guid.NewGuid();
            ClienteId = usuarioId;
            FechaCreacion = DateTime.UtcNow;
            Estado = EstadoOrden.PendientePago;
        }

        public void AgregarItem(int productoId, string nombre, int cantidad, decimal precio, int artesanoId)
        {
            Items.Add(new OrdenItem
            {
                ProductoId = productoId,
                NombreProducto = nombre,
                Cantidad = cantidad,
                PrecioUnitario = precio,
                ArtesanoId = artesanoId
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
        public void MarcarComoPagada(long paymentId)
        {
            if (Estado == EstadoOrden.Pagada)
                return; // idempotencia

            if (Estado != EstadoOrden.PendientePago)
                throw new InvalidOperationException(
                    "Solo se puede pagar una orden pendiente");

            Estado = EstadoOrden.Pagada;
            FechaPago = DateTime.UtcNow;
            //MercadoPagoPaymentId = paymentId;
        }

        public void MarcarComoCancelada()
        {
            if (Estado != EstadoOrden.PendientePago)
                throw new InvalidOperationException("Solo se puede cancelar una orden pendiente");

            Estado = EstadoOrden.Cancelada;
        }

        public void MarcarComoRechazada()
        {
            if (Estado != EstadoOrden.PendientePago)
                throw new InvalidOperationException("Solo se puede rechazar una orden pendiente");

            Estado = EstadoOrden.Rechazada;
        }

        public void MarcarComoPendiente()
        {
            Estado = EstadoOrden.PendientePago;
        }

        //public void AsignarMercadoPagoSeller(string mpUserId)
        //{
        //    MercadoPagoSellerUserId = mpUserId;
        //}

        //public void AsignarPaymentId(long paymentId)
        //{
        //    MercadoPagoPaymentId = paymentId;
        //    FechaPago = DateTime.UtcNow;
        //}
    }


}
