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
        public List<FacturaNoFiscal> ventas { get; set; } = new List<FacturaNoFiscal>();
        public List<Producto> productos { get; set; } = new List<Producto>();
        public string? foto { get; set; }
        //public List<PedidoPersonalizado> pedidosArtesano { get; set; }
        // ============================
        // MERCADO PAGO - MARKETPLACE
        // ============================

        public long? MercadoPagoUserId { get;  set; }

        public string? MercadoPagoAccessToken { get;  set; }

        public string? MercadoPagoRefreshToken { get;  set; }

        public DateTime? MercadoPagoTokenExpira { get;  set; }

        public bool TieneMercadoPagoConectado =>
            !string.IsNullOrEmpty(MercadoPagoAccessToken);

        // ============================
        // MÉTODOS DE DOMINIO
        // ============================

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
           
    }
     
    }
