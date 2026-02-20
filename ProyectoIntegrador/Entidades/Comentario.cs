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
    public class Comentario : IValidable
    {
        public int id { get; set; }
        [Required(ErrorMessage = "el contenido es requerido")]
        public string contenido { get; set; }
        public int? clienteId { get; set; }
        public Cliente? cliente { get; set; }
        public int? artesanoId { get; set; }
        public Artesano? artesano { get; set; }



        public void Validar()
        {
            validarContenido();
        }

        public void validarContenido()
        {
            if ((string.IsNullOrWhiteSpace(contenido)))
            {
                throw new ContenidoVacioException();
            }
        }
    }
}
