using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.EntityFrameWork.Repositorios
{
    public class CategoriaEFRepsoitorio : ICategoriaRepositorio
    {

        private readonly ProyectoDBContext _contexto;

        public CategoriaEFRepsoitorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }

        public void Agregar(Categoria entidad)
        {
            throw new NotImplementedException();
        }

        public void Editar(Categoria entidad)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Categoria Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Categoria> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
