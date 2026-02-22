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
    public class EliminarUsuarioCasoDeUso : IEliminarUsuario
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IComentarioRepositorio _comentarioRepo;

        public EliminarUsuarioCasoDeUso(IUsuarioRepositorio usuarioRepositorio, IComentarioRepositorio comentarioRepo)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _comentarioRepo = comentarioRepo;
        }

        public void Ejecutar(string email)
        {
            var usuario = _usuarioRepositorio.BuscarPorEmail(email);

            if (usuario == null)
                throw new Exception("El usuario no existe.");

            if(usuario is Artesano artesano)
            {
                _comentarioRepo.EliminarPorArtesano(artesano.id);
            }

            _usuarioRepositorio.Eliminar(usuario.id);
        }
    }
}
