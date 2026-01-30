using ProyectoIntegrador.LogicaNegocio.Excepciones;
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
        public List<FacturaNoFiscalArtesano> ventas { get; set; } = new List<FacturaNoFiscalArtesano>();
        public List<Producto> productos { get; set; } = new List<Producto>();
        public string? foto { get; set; }
        public bool bloqueado { get; set; }
        //public List<PedidoPersonalizado> pedidosArtesano { get; set; }
        
        // MERCADO PAGO - MARKETPLACE
        public long? MercadoPagoUserId { get; set; }

        public string? MercadoPagoAccessToken { get; set; }

        public string? MercadoPagoRefreshToken { get; set; }

        public DateTime? MercadoPagoTokenExpira { get; set; }





        public bool TieneMercadoPagoConectado =>
            !string.IsNullOrEmpty(MercadoPagoAccessToken);

        public void ConectarMercadoPago(
            long userId,
            string accessToken,
            string refreshToken,
            int expiresInSeconds)
        {
            MercadoPagoUserId = userId;
            MercadoPagoAccessToken = accessToken;
            MercadoPagoRefreshToken = refreshToken;
            MercadoPagoTokenExpira = DateTime.UtcNow.AddSeconds(expiresInSeconds);
        }

        public void DesconectarMercadoPago()
        {
            MercadoPagoUserId = null;
            MercadoPagoAccessToken = null;
            MercadoPagoRefreshToken = null;
            MercadoPagoTokenExpira = null;
        }
        public void ValidarTelefono(string telefono)
        {
            if(telefono != null) {
                if (!telefono.All(char.IsDigit))
                    throw new TelefonoUsuarioException("El teléfono solo puede contener números.");
            }
        }
        
        public void estaBloquado()
        {
            if (this.bloqueado == true)
            {
                throw new UsuarioBloqueadoException();
            }
        }

        public int Totalventas()
        {
            return ventas.Count;
        }

        public int VentasMesActual()
        {
            DateTime hoy = DateTime.UtcNow;
            DateTime inicioMes = new DateTime(hoy.Year, hoy.Month, 1);

            return ventas.Count(v => v.Fecha >= inicioMes && v.Fecha <= hoy);
        }

        public int VentasMesAnterior()
        {
            DateTime hoy = DateTime.UtcNow;
            DateTime inicioMesActual = new DateTime(hoy.Year, hoy.Month, 1);
            DateTime inicioMesAnterior = inicioMesActual.AddMonths(-1);

            return ventas.Count(v =>
                v.Fecha >= inicioMesAnterior &&
                v.Fecha < inicioMesActual
            );
        }

        public double VariacionVentasMensual()
        {
            int mesActual = VentasMesActual();
            int mesAnterior = VentasMesAnterior();

            if (mesAnterior == 0)
                return mesActual > 0 ? 100 : 0;

            return ((double)(mesActual - mesAnterior) / mesAnterior) * 100;
        }

        public int TotalventasXAno()
        {
            DateTime hoy = DateTime.UtcNow;
            DateTime inicioAno = new DateTime(hoy.Year, 1, 1);

            return ventas.Count(v => v.Fecha >= inicioAno && v.Fecha <= hoy);
        }

        public List<Producto> ProductosMasVendidos()
        {
            var topProductosIds = ventas
            .SelectMany(v => v.Orden.Items)
            .Where(i => i.ArtesanoId == this.id)
            .GroupBy(i => i.ProductoId)
            .OrderByDescending(g => g.Sum(i => i.Cantidad))
            .Take(3)
            .Select(g => g.Key)
            .ToHashSet();

            return productos
                .Where(p => topProductosIds.Contains(p.id))
                .ToList();
        }


    }
     
    }
