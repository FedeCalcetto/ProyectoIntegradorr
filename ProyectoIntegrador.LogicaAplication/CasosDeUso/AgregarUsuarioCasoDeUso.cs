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
    public class AgregarUsuarioCasoDeUso : IAgregarUsuario
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public AgregarUsuarioCasoDeUso(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public void Ejecutar(Usuario u)
        {
            _usuarioRepositorio.Agregar(u);
        }
    }
}
