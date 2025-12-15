using ProyectoIntegrador.LogicaNegocio.Interface.Validacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class CarritoItem
    {
        public int id { get; set; }
        public int carritoId { get; set; }
        public Carrito carrito { get; set; }
        public int productoId { get; set; }
        public Producto producto { get; set; }
        public int cantidad { get; set; }
    
        public CarritoItem() { }
        public CarritoItem(int carritoId, int productoId, int cantidad)
        {
            this.carritoId = carritoId;
            this.productoId = productoId;
            this.cantidad = cantidad;
        }

        public void validarCantidad(int cantidad) {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero");
        }

        public void SumarCantidad(int cantidadASumar)
        {
            if (cantidadASumar <= 0)
                throw new ArgumentException("La cantidad a sumar debe ser mayor a cero");

            cantidad += cantidadASumar;
        }

        public void RestarCantidad(int cantidadARestar)
        {
            if (cantidadARestar <= 0)
                throw new ArgumentException("La cantidad a restar debe ser mayor a cero");

            if (cantidadARestar > cantidad)
                throw new InvalidOperationException(
                    "No se puede restar más cantidad de la que existe en el carrito"
                );

            cantidad -= cantidadARestar;
        }

        public bool QuedaEnCero()
        {
            return cantidad == 0;
        }

    
    }
}
