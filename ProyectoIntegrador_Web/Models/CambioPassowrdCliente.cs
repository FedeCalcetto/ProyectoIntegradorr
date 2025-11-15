using System.ComponentModel.DataAnnotations;

namespace ProyectoIntegrador_Web.Models
{
    public class CambioPassowrdCliente
    {
        public string passwordActual { get; set; }
        [StringLength(30, MinimumLength = 10, ErrorMessage = "La contraseña debe tener entre 10 y 30 caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string PasswordRepetida { get; set; }
    }
}
