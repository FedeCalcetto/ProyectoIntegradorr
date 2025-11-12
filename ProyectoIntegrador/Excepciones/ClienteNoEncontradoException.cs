using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Excepciones
{
    public class ClienteNoEncontradoException : Exception
    {
        public ClienteNoEncontradoException() : base("No se encontró al cliente") 
        { 
        }
    }
}
