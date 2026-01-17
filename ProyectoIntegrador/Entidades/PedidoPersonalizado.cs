using ProyectoIntegrador.LogicaNegocio.Interface.Validacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class PedidoPersonalizado
    {
        public int Id { get; set; }

        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaFinalizacion { get; set; }

        public EstadoPedido Estado { get; set; }

        // Relaciones
        public Cliente? Cliente { get; set; }
        public int? ClienteId { get; set; }
        public Artesano? Artesano { get; set; }
        public int? ArtesanoId { get; set; }
    }
}
    