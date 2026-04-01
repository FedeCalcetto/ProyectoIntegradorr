using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class CalificacionArtesano 
    {
        public int id { get; set; }
        public int usuarioId { get; set; }
        public decimal puntaje { get; set; }
        public DateTime fecha { get; set; } = DateTime.Now;
        public int artesanoId { get; set; }


        public static CalificacionArtesano ParaArtesano(int artesanoId, int usuarioId, decimal puntaje)
        {
            return new CalificacionArtesano
            {
                artesanoId = artesanoId,
                usuarioId = usuarioId,
                puntaje = puntaje
            };
        }

    }
}
