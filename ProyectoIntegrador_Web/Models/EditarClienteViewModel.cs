using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace ProyectoIntegrador_Web.Models
{
    public class EditarClienteViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
        public string Email { get; set; } 

        [Required(ErrorMessage = "El domicilio es obligatorio")]
        public string Domicilio { get; set; }
        [Required(ErrorMessage = "El departamento es obligatorio")]
        public string Departamento { get; set; }
        [Required(ErrorMessage = "El barrio es obligatorio")]
        public string Barrio { get; set; }
        public List<string>? DepartamentosOpciones { get; set; }
        public string Foto { get; set; }
    }
}
