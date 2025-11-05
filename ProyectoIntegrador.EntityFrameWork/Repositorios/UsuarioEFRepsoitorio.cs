using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Excepciones;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.EntityFrameWork.Repositorios
{
    public class UsuarioEFRepsoitorio : IUsuarioRepositorio
    {

        private readonly ProyectoDBContext _contexto;

        public UsuarioEFRepsoitorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }

        public void Agregar(Usuario entidad)
        {

            if (!existeEmail(entidad.email.email))
            {
                entidad.Validar();
                _contexto.Usuarios.Add(entidad);
                _contexto.SaveChanges();
            }
            else
            {
                throw new ExisteEmailException();
            }


        }

        public bool existeEmail(string email)
        {

            var normalizado = email.ToLower().Trim();
            return _contexto.Usuarios
                .Any(u => u.email.email.ToLower().Trim() == normalizado);
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
