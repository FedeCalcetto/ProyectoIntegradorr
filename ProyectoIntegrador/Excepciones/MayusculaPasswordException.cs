using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Excepciones
{
    public class MayusculaPasswordException : Exception
    {

        public MayusculaPasswordException() : base("la contraseña debe tener una mayuscula y otra en minuscula") { }

    }
}
