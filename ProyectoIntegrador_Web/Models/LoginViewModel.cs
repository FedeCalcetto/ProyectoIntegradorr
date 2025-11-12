using System.ComponentModel.DataAnnotations;

namespace ProyectoIntegrador_Web.Models
{
    public class LoginViewModel
    {

        [Required]
        public string Email { get; init; }
        [Required]
        [StringLength(30, MinimumLength = 10, ErrorMessage = "La contraseña no cumple con los requisitos mínimos.")]
        public string Password { get; init; }

    }
}
