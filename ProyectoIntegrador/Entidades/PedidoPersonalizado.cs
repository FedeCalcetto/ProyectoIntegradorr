using ProyectoIntegrador.LogicaNegocio.Interface.Validacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class PedidoPersonalizado : IValidable
    {
        public int id { get; set; }
        [Required(ErrorMessage = "la descripción es requerida es requerida")]
        public string descripcion { get; set; }
        public int? clienteId { get; set; }
        public Cliente? cliente { get; set; }
        public int? artesanoId { get; set; }
        public Artesano? artesano { get; set; }

        public void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
