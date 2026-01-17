using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class FacturaNoFiscalArtesano : FacturaNoFiscal
    {
        public int ArtesanoId { get; set; }
        public Artesano Artesano { get; set; }
    }
}
