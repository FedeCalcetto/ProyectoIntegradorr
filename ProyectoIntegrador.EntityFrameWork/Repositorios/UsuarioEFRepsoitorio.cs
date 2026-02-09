using Microsoft.EntityFrameworkCore;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Excepciones;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador.LogicaNegocio.ValueObjects;
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
            var usuario = _contexto.Usuarios.FirstOrDefault(u => u.id == id);

            if (usuario == null)
                throw new UsuarioNullException();

            _contexto.Usuarios.Remove(usuario);
            _contexto.SaveChanges();
        }

        public Usuario Obtener(int id)
        {
            return _contexto.Usuarios.FirstOrDefault(u => u.id == id);
        }

        public IEnumerable<Usuario> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        /* public Usuario Login(string email, string password)
         {

             Usuario usuario = _contexto.Usuarios.FirstOrDefault(u => u.email.email.ToLower().Trim() == email.ToLower().Trim()
                                 && u.password == password);

             return usuario;
         }*/   // se eñimina ya que compara el password en texto plano. esto ahora lo hace el caso de uso de login.

        public Usuario BuscarPorEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;


            var usuario = _contexto.Usuarios
                .FirstOrDefault(u => u.email.email.ToLower().Trim() == email.ToLower().Trim());

            if(usuario is Artesano artesano)
            {
                artesano.estaBloquado();
            }

            return usuario;
        }

        public void Actualizar(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            usuario.Validar(); 

            _contexto.Usuarios.Update(usuario);
            _contexto.SaveChanges();
        }

        public List<Usuario> BusquedaDeUsuarios(string filtro)
        {
            return _contexto.Usuarios
                .Where(u =>
                    (u is Cliente || u is Artesano) &&
                    (u.nombre.Contains(filtro) || u.apellido.Contains(filtro))
                )
                .ToList();
        }

        public Usuario BuscarPorToken(string token)
        {
            return _contexto.Usuarios
       .FirstOrDefault(u =>
           u.TokenVerificacionEmail == token);
        }

        
    }
}
