using ProyectoIntegrador.LogicaNegocio.Entidades;
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
    }
}
