using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Excepciones
{
    public class numeroProductoException : Exception
    {
        public numeroProductoException() : base("el nombre no puede tener numeros")
        {
        }
    }
}
