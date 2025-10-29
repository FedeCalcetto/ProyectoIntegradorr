using ProyectoIntegrador.LogicaNegocio.Interface.Validacion;
using ProyectoIntegrador.LogicaNegocio.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public abstract class Usuario : IValidable
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "El apellido es requerido")]
        public string apellido { get; set; }
        [Required(ErrorMessage = "El email es requerido")]
        public Email email { get; set; }
        [Required(ErrorMessage = "la contraseña es requerida")]
        [StringLength(30, MinimumLength = 10, ErrorMessage = "La contraseña debe tener entre 10 y 30 caracteres")]
        public string password { get; set; }

        public void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
