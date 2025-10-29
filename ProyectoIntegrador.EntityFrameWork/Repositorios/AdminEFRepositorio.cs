using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.EntityFrameWork.Repositorios
{
    public class AdminEFRepositorio : IAdminRepositorio
    {

        private readonly ProyectoDBContext _contexto;

        public AdminEFRepositorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }

        public void Agregar(Admin entidad)
        {
            throw new NotImplementedException();
        }

        public void Editar(Admin entidad)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Admin Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Admin> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
