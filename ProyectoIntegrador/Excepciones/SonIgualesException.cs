using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Excepciones
{
    public class SonIgualesException : Exception
    {
        public SonIgualesException() : base("la nueva contraseña no puede ser igual a la anterior") 
        {
        }
    }
}
