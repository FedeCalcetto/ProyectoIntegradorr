using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ProyectoIntegrador_Web.Models
{
    public class EditarClienteViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; }

        [ValidateNever]
        public string Email { get; set; } 

        [Required(ErrorMessage = "El domicilio es obligatorio")]
        public string Domicilio { get; set; }
        [Required(ErrorMessage = "El departamento es obligatorio")]
        public string Departamento { get; set; }
        [Required(ErrorMessage = "El barrio es obligatorio")]
        public string Barrio { get; set; }
        public List<string>? DepartamentosOpciones { get; set; }
        public string? Foto { get; set; }

        [ValidateNever]// el model no valida esto, por ende no valida codigo de seguridad, etc.
        public EliminarCuentaViewModel EliminarCuenta { get; set; } //para que funcione el modal
    }
}
