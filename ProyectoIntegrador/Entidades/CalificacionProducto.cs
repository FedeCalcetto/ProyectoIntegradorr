using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class CalificacionProducto 
    {
        public int id { get; set; }
        public int usuarioId { get; set; }
        public decimal puntaje { get; set; }
        public DateTime fecha { get; set; } = DateTime.Now;
        public int productoId { get; set; }


        public static CalificacionProducto ParaProducto(int productoId, int usuarioId, decimal puntaje)
        {
            return new CalificacionProducto
            {
                productoId = productoId,
                usuarioId = usuarioId,
                puntaje = puntaje
            };
        }

    }
}
