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
    public class BusquedaDeUsuariosCasoDeUso : IBusquedaDeUsuarios
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public BusquedaDeUsuariosCasoDeUso(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public List<Usuario> Ejecutar(string filtro)
        {
            return _usuarioRepositorio.BusquedaDeUsuarios(filtro);
        }
    }
}
