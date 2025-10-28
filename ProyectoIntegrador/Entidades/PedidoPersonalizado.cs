using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class PedidoPersonalizado
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public Cliente cliente { get; set; }
    }
}
