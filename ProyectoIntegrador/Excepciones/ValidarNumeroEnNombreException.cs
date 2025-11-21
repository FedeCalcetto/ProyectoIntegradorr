using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Excepciones
{
    public class ValidarNumeroEnNombreException : Exception
    {
        public ValidarNumeroEnNombreException() : base("El nombre y el apellido no puede tener numeros")
        {

        }
    }
}
