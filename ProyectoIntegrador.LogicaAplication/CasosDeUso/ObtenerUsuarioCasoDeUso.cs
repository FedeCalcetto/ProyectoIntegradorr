using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador.LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class ObtenerUsuarioCasoDeUso : IObtenerUsuario
    {
        private readonly IUsuarioRepositorio _usuarioRepo;

        public ObtenerUsuarioCasoDeUso(IUsuarioRepositorio usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }

        public Usuario Ejecutar(string email)
        {
            return _usuarioRepo.BuscarPorEmail(email);
        }

        public Usuario ObtenerPorId(int id)
        {
            return _usuarioRepo.Obtener(id);
        }
    }
}
