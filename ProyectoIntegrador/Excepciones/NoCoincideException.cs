using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Excepciones
{
    public class NoCoincideException : Exception
    {
        public NoCoincideException() : base("la contraseña nueva no coincide")
        { 
        }
    }
}
