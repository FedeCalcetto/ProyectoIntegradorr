using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Excepciones
{
    public class ContraActualException : Exception
    {
        public ContraActualException() : base("Esa no es la contraseña actual")
        {
        }
    }
}
