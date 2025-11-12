using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class Reporte
    {
        public int id { get; set; }
        [Required(ErrorMessage = "el contenido es requerido")]
        public string razon { get; set; }
        public int? clienteId { get; set; }
        public Cliente? cliente { get; set; }
        public int? artesanoId { get; set; }
        public Artesano? artesano { get; set; }
        public int? productoId { get; set; }
        public Producto? producto { get; set; }
    }
}
