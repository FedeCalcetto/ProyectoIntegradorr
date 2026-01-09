using ProyectoIntegrador.LogicaAplication.Servicios;
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
       // [StringLength(30, MinimumLength = 10, ErrorMessage = "La contraseña debe tener entre 10 y 30 caracteres")] esto solo en ViewModel
        public string password { get; /*private*/ set; } 
        public string rol { get; set; } 


        public string? CodigoVerificacion { get; set; }
        public bool Verificado { get; set; }


        // Propiedades para la verificación de email via Token (clickear el link para validar automaticamente) //
        public string? TokenVerificacionEmail { get; set; }
        public DateTime? TokenVerificacionEmailExpira { get; set; }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////



        public void Validar()
        {
            validarNombres();
        }

        public void validarNombres()
        {
            if (nombre.Length < 1 || apellido.Length < 1)
            {
                throw new validarNombreException();
            }
            if(nombre.Any(char.IsDigit) || apellido.Any(char.IsDigit))
            {
                throw new ValidarNumeroEnNombreException();
            }
        }

        public void validarContra(string contraNueva, string contraRepetida, string contraActual)
        {
            ValidarContraseñaLongitud(contraNueva, contraRepetida);
            ValidarPasswordMaysucula(contraNueva, contraRepetida);
            ValidarNumero(contraNueva, contraRepetida);
            SonIguales(contraNueva, contraRepetida, contraActual);
        }

        private void ValidarContraseñaLongitud(string contraNueva, string contraRepetida)
        {
            if (contraNueva.Length < 10 || contraNueva.Length > 30 || contraRepetida.Length < 10 || contraRepetida.Length > 30)
            {
                throw new passwordUsuarioException();
            }
        }

        private void ValidarPasswordMaysucula(string contraNueva, string contraRepetida)
        {
            if (!contraNueva.Any(char.IsUpper) || !contraNueva.Any(char.IsLower) || !contraRepetida.Any(char.IsUpper) || !contraRepetida.Any(char.IsUpper))
            {
                throw new MayusculaPasswordException();
            }

        }

        private void ValidarNumero(string contraNueva, string contraRepetida)
        {
            if (!contraNueva.Any(char.IsDigit) || !contraRepetida.Any(char.IsDigit))
            {
                throw new numeroPassowordException();
            }
        }

        private void SonIguales(string contraNueva, string contraRepetida, string contraActual)
        {
            if (!VerificarPassword(contraActual))
            {
                throw new ContraActualException();
            }


            if (VerificarPassword(contraNueva))
            {
                throw new SonIgualesException();
            }


            if (!contraNueva.Equals(contraRepetida))
            {
                throw new NoCoincideException();
            }
        }


        public void SetPasswordInicial(string passwordPlano)
        {
            password = PasswordHasher.Hash(passwordPlano);
        }

        public bool VerificarPassword(string passwordPlano)
        {
            return PasswordHasher.Verify(passwordPlano, password);
        }

    }
}
