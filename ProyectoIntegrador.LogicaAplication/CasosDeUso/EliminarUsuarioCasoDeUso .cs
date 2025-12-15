using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class EliminarUsuarioCasoDeUso : IEliminarUsuario
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public EliminarUsuarioCasoDeUso(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public void Ejecutar(string email)
        {
            var usuario = _usuarioRepositorio.BuscarPorEmail(email);

            if (usuario == null)
                throw new Exception("El usuario no existe.");

            _usuarioRepositorio.Eliminar(usuario.id);
        }
    }
}
