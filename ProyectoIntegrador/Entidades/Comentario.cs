using ProyectoIntegrador.LogicaNegocio.Interface.Validacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class Comentario : IValidable
    {
        public int id { get; set; }
        [Required(ErrorMessage = "el contenido es requerido")]
        public string contenido { get; set; }

        public Cliente cliente { get; set; }
        public Artesano artesano { get; set; }
        public Producto producto { get; set; }
        public bool productoReportado { get; set; }
        public bool artesanoReportado { get; set; }

        //Si el usuario reporta un rpoducto el booleano queda en true y el producto no es null
        //y asi en viceversa con el artesano
        // y si no  es un reporte quedan los 2 en false


        public void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
