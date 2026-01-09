using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ProyectoIntegrador_Web.Models
{
    public class EditarArtesanoViewModel
    {
        [ValidateNever]
        public string Descripcion { get; set; }
        [ValidateNever]
        public string Telefono { get; set; }


        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; }

        [ValidateNever]
        public string Email { get; set; }

        [ValidateNever]
        public string Password { get; set; }


        public string? Foto { get; set; }
        [ValidateNever]// el model no valida esto, por ende no valida codigo de seguridad, etc.
        public EliminarCuentaViewModel? EliminarCuenta { get; set; }//para que funcione el modal

    }
}
