using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class LoginCasoDeUso : ILogin
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public LoginCasoDeUso(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public Usuario Ejecutar(string email, string password)
        {
            Usuario usuario = _usuarioRepositorio.BuscarPorEmail(email);

            if (usuario == null)
                return null;

            if (!usuario.VerificarPassword(password)) // esto antes lo hacia el repositorio ahora se verifica aca con el metodo de la entidad usuario.
                return null;

            return usuario;
        }


    }
}
