using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Excepciones
{
    public class passwordUsuarioException : Exception
    {

        public passwordUsuarioException() : base("la contraseña debe tener por lo menos 10 caracteres hasta 30 caracteres")
        {

        }
    }
}
