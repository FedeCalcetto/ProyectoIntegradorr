using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public  class FacturaNoFiscalCliente : FacturaNoFiscal
    {
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

    }
}
