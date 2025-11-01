using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Excepciones
{
    public class ExisteEmailException : Exception
    {

        public ExisteEmailException() : base("Ya existe ese mail"){ }
    }
}
