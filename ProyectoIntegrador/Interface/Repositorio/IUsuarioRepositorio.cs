using ProyectoIntegrador.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Interface.Repositorio
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
       // Usuario Login(string email, string password);
        Usuario BuscarPorEmail(string email);
        void Actualizar(Usuario usuario);
        List<Usuario> BusquedaDeUsuarios(string filtro);
        Usuario BuscarPorToken(string token);
    }
}
