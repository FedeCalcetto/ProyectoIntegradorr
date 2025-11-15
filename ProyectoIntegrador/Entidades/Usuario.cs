using ProyectoIntegrador.LogicaNegocio.Excepciones;
using ProyectoIntegrador.LogicaNegocio.Interface.Validacion;
using ProyectoIntegrador.LogicaNegocio.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public abstract class Usuario : IValidable
    {
        public int id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "El apellido es requerido")]
        public string apellido { get; set; }
        [Required(ErrorMessage = "El email es requerido")]
        public Email email { get; set; }
        [Required(ErrorMessage = "la contraseña es requerida")]
        [StringLength(30, MinimumLength = 10, ErrorMessage = "La contraseña debe tener entre 10 y 30 caracteres")]
        public string password { get; set; }
        public string rol { get; set; }

        public void Validar()
        {
            validarContraseñaLongitud();
            validarPasswordMaysucula();
            validarNombres();
            validarNumero();
        }


        public void validarContraseñaLongitud()
        {
            if (password.Length < 10 || password.Length > 30)
            {
                throw new passwordUsuarioException();
            }
        }

        public void validarPasswordMaysucula()
        {
            if (!password.Any(char.IsUpper) || !password.Any(char.IsLower))
            {
                throw new MayusculaPasswordException();
            }
            {

            }
        }

        public void validarNombres()
        {
            if (nombre.Length < 1 || apellido.Length < 1)
            {
                throw new validarNombreException();
            }
        }

        public void validarNumero()
        {
            if (!password.Any(char.IsDigit))
            {
                throw new numeroPassowordException();
            }
        }
    }
}
