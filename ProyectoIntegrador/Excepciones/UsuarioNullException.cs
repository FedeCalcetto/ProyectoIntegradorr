using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Excepciones
{
    public class UsuarioNullException : Exception
    {
        public UsuarioNullException() : base("No se encontró al usuario") 
        {
        }

        public UsuarioNullException(string? message) : base(message)
        {
        }

        public UsuarioNullException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
