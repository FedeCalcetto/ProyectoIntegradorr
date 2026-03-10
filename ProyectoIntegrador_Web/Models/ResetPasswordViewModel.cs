namespace ProyectoIntegrador_Web.Models;
using System.ComponentModel.DataAnnotations;

    public class ResetPasswordViewModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string NuevaPassword { get; set; }

        [Required]
        public string RepetirPassword { get; set; }
    }

