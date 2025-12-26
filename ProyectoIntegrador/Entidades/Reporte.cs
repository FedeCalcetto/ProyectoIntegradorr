using ProyectoIntegrador.LogicaNegocio.Excepciones;
using ProyectoIntegrador.LogicaNegocio.Interface.Validacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class Reporte :IValidable
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Debe ingresar un motivo para el reporte")]
        public string razon { get; set; }
        public int? clienteId { get; set; }
        public Cliente? cliente { get; set; }
        public int? artesanoId { get; set; }
        public Artesano? artesano { get; set; }
        public int? productoId { get; set; }
        public Producto? producto { get; set; }
        public DateTime fecha { get; set; }

        public void Validar()
        {
            if(razon.Length < 1)
            {
                throw new LongitudRazonException();
            }
        }
    }
}
