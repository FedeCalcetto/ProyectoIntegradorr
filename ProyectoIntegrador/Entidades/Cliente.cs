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
        public int id { get; set; }

        public Direccion direccion { get; set; }
        public List<Comentario> comentarios { get; set; }
        public List<Factura> compras { get; set; } = new List<Factura>();
        public List<Producto> productosFavoritos { get; set; }

    }
}
