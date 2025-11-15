using System.ComponentModel.DataAnnotations;

namespace ProyectoIntegrador_Web.Models
{
    public class NuevaPasswordViewModel
    {
        [Required(ErrorMessage = "Debe ingresar una contraseña.")]
        [StringLength(30, MinimumLength = 10, ErrorMessage = "La contraseña debe tener entre 10 y 30 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe confirmar la contraseña.")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}
