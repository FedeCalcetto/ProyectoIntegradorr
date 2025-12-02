using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Excepciones
{
    public class PrecioStockException : Exception
    {
        public PrecioStockException() : base("Tanto el precio como el stock deben ser mayores a 0")
        {
        }
    }
}
