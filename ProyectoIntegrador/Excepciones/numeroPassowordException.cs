using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Excepciones
{
    public class numeroPassowordException : Exception
    {
        public numeroPassowordException() : base("la contraseña debe tener al menos un numero"){ }
    }
}
