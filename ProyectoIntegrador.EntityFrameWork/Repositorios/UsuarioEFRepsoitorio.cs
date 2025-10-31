using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.EntityFrameWork.Repositorios
{
    public class AdminEFRepsoitorio : IUsuarioRepositorio
    {

        private readonly ProyectoDBContext _contexto;

        public AdminEFRepsoitorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }

        public void Agregar(Usuario entidad)
        {
            throw new NotImplementedException();
        }

        public void Editar(Usuario entidad)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Usuario Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public Usuario Login(string email, string password)
        {

            Usuario usuario = _contexto.Usuarios.FirstOrDefault(u => u.email.email.ToLower().Trim() == email.ToLower().Trim()
                                && u.password == password);

            return usuario;
        }

    }
}
