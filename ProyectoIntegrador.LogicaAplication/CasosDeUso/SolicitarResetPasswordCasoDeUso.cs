using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class SolicitarResetPasswordCasoDeUso : ISolicitarResetPassword
    {
        private readonly IUsuarioRepositorio _repo;
        private readonly IEmailService _email;

        public SolicitarResetPasswordCasoDeUso(IUsuarioRepositorio repo, IEmailService email)
        {
            _repo = repo;
            _email = email;
        }

        public async Task Ejecutar(string email, string baseUrl)
        {
            var u = _repo.BuscarPorEmail(email);
            if (u == null) return;

            var token = Guid.NewGuid().ToString("N");

            u.TokenResetPassword = token;
            u.TokenResetPasswordExpira = DateTime.UtcNow.AddMinutes(30);

            _repo.Actualizar(u);

            var link = $"{baseUrl}/Login/ResetPassword?token={token}";
            await _email.EnviarResetPasswordAsync(email, link);
        }
    }
}
