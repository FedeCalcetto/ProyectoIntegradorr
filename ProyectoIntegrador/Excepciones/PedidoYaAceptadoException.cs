using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Excepciones
{
    public class PedidoYaAceptadoException : Exception
    {
        public PedidoYaAceptadoException() : base("Este pedido ya fue aceptado por otro artesano.")
        {
        }
    }
}
