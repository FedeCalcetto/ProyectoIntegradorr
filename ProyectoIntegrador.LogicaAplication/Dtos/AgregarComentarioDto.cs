using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Dtos
{
    public class AgregarComentarioDto
    {
        [Required(ErrorMessage = "Debe ingresar un comentario")]
        public string contenido { get; set; }
    }
}
