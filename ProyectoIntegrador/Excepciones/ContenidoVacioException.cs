using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Excepciones
{
    public class ContenidoVacioException : Exception
    {
        public ContenidoVacioException() : base("el comentario no puede estar vacío")
        {
        }
    }
}
