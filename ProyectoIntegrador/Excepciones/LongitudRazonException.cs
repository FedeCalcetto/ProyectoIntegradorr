using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Excepciones
{
    public class LongitudRazonException : Exception
    {
        public LongitudRazonException() :base("Debe ingresar una razón")
        {
        }
    }
}
