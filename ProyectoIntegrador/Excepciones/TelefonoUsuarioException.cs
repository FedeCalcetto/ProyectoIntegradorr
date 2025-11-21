using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Excepciones
{
    public class TelefonoUsuarioException: Exception
    {

        public TelefonoUsuarioException() : base()
        {
        }

        public TelefonoUsuarioException(string mensaje) : base(mensaje)
        {
        }
    }
}
