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

        public List<Artesano> artesanosSeguidos { get; set; }
        public List<Factura> compras { get; set; } = new List<Factura>();
        public List<Producto> productosFavoritos { get; set; }
        //public List<PedidoPersonalizado> pedidosCliente { get; set; }
        public Direccion? direccion { get; set; }
        public void validarEditar()
        {

        }

        public void valdiarDireccion()
        {
            if (direccion.barrio.IsNullOrEmpty() || direccion.departamento.IsNullOrEmpty() || direccion.domicilio.IsNullOrEmpty())
            {
                throw new DireccionException();
            }
        }
    }
}
