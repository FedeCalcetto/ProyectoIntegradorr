using System.ComponentModel.DataAnnotations;

namespace ProyectoIntegrador_Web.Models
{
    public class CambioPassowrdViewModel
    {
        [Required(ErrorMessage = "Debe ingresar su contraseña actual")]
        public string passwordActual { get; set; }
        [Required(ErrorMessage = "Debe ingresar una contraseña.")]
        [StringLength(30, MinimumLength = 10, ErrorMessage = "La contraseña debe tener entre 10 y 30 caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Debe repetir la contraseña")]
        public string PasswordRepetida { get; set; }
    }
}
