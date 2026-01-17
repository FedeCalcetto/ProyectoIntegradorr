using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Excepciones
{
    public class UsuarioBloqueadoException : Exception
    {
        public UsuarioBloqueadoException() : base("su usuario fue bloqueado comuniquese vía mail a hechoxmiweb@gmail.com")
        {
            

        }
    }
}
