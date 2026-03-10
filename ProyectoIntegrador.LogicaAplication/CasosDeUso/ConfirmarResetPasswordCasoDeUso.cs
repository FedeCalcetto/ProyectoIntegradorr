using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class ConfirmarResetPasswordCasoDeUso : IConfirmarResetPassword
    {
        private readonly IUsuarioRepositorio _repo;

        public ConfirmarResetPasswordCasoDeUso(IUsuarioRepositorio repo)
        {
            _repo = repo;
        }

        public void Ejecutar(string token, string nueva, string repetir)
        {
            var u = _repo.BuscarPorTokenReset(token);
            if (u == null)
                throw new Exception("Link inválido o expirado.");

            if (u.TokenResetPasswordExpira == null || u.TokenResetPasswordExpira.Value < DateTime.UtcNow)
                throw new Exception("Link inválido o expirado.");

            if (nueva.Length < 10 || nueva.Length > 30)
                throw new Exception("La contraseña debe tener entre 10 y 30 caracteres.");

            if (!nueva.Any(char.IsUpper) || !nueva.Any(char.IsLower))
                throw new Exception("La contraseña debe tener mayúsculas y minúsculas.");

            if (!nueva.Any(char.IsDigit))
                throw new Exception("La contraseña debe incluir al menos un número.");

            if (nueva != repetir)
                throw new Exception("Las contraseñas no coinciden.");

            // set (usa PasswordHasher)
            u.SetPasswordInicial(nueva);

            // invalidar token
            u.TokenResetPassword = null;
            u.TokenResetPasswordExpira = null;

            _repo.Actualizar(u);
        }
    }
}
