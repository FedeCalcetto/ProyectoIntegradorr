using Microsoft.IdentityModel.Tokens;
using ProyectoIntegrador.LogicaNegocio.Excepciones;
using ProyectoIntegrador.LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class Cliente : Usuario
    {

        public List<Artesano> artesanosSeguidos { get; set; } = new List<Artesano>();
        public ICollection<FacturaNoFiscalCliente> compras { get; set; }
        public List<Producto> productosFavoritos { get; set; } = new();
        //public List<PedidoPersonalizado> pedidosCliente { get; set; }
        public Direccion? direccion { get; set; }
        public string? foto { get; set; }
        public bool bloqueado { get; set; }
        public void validarEditar()
        {
            valdiarDireccion();
            validarNombres();
        }

        public void valdiarDireccion()
        {
            if (direccion.barrio.Equals("") ||
                direccion.departamento.Equals("") ||
                direccion.domicilio.Equals(""))
            {
                throw new DireccionException();
            }

        }

        public void agregarArtesano(Artesano a)
        {
            artesanosSeguidos.Add(a);
        }
        public void eliminarArtesano(Artesano a)
        {
            artesanosSeguidos.Remove(a);
        }
    }
}
