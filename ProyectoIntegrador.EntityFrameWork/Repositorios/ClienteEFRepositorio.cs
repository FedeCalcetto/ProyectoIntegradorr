using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.EntityFrameWork.Repositorios
{
    public class ClienteEFRepositorio : IClienteRepositorio
    {

        private readonly ProyectoDBContext _contexto;

        public ClienteEFRepositorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }

        public void Agregar(Cliente entidad)
        {
            throw new NotImplementedException();
        }

        public void Editar(Cliente entidad)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Cliente Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cliente> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
