using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Excepciones
{
    public class validarNombreException : Exception
    {
        public validarNombreException() : base("ingrese nombre y apellido") { }
    }
}
