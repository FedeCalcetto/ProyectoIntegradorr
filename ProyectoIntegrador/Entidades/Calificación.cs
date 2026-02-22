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
        public int productoId { get; set; }
        public int usuarioId { get; set; }
        public decimal puntaje { get; set; } 
        public DateTime fecha { get; set; } = DateTime.Now;



        private Calificación() { }
        public Calificación(int proId, int uId, decimal ptj)
        {
            productoId = proId;
            usuarioId = uId;
            puntaje = ptj;
        }

    }
}
