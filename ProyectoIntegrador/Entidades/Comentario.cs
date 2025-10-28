using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class Comentario
    {
        public string contenido { get; set; }

        public Cliente cliente { get; set; }
    }
}
