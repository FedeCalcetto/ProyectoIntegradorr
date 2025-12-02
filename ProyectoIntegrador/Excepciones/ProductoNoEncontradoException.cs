using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Excepciones
{
    public class ProductoNoEncontradoException : Exception
    {
        public ProductoNoEncontradoException() : base("No se encontró el producto")
        {
        }
    }
}
