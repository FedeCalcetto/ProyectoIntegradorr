using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class Calificación
    {
        public int id { get; set; }
        public int? productoId { get; set; }
        public int usuarioId { get; set; }
        public decimal puntaje { get; set; } 
        public DateTime fecha { get; set; } = DateTime.Now;
        public int? artesanoId { get; set; }

        private Calificación() { }
        public static Calificación ParaProducto(int productoId, int usuarioId, decimal puntaje)
        {
            return new Calificación
            {
                productoId = productoId,
                usuarioId = usuarioId,
                puntaje = puntaje
            };
        }

        public static Calificación ParaArtesano(int artesanoId, int usuarioId, decimal puntaje)
        {
            return new Calificación
            {
                artesanoId = artesanoId,
                usuarioId = usuarioId,
                puntaje = puntaje
            };
        }

    }
}
