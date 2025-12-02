using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.EntityFrameWork.Repositorios
{
    public class SubCategoriaEFRepositorio : ISubCategoriaRepositorio
    {

        private readonly ProyectoDBContext _contexto;

        public SubCategoriaEFRepositorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }
        public void Agregar(SubCategoria entidad)
        {
            throw new NotImplementedException();
        }

        public void Editar(SubCategoria entidad)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public SubCategoria Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubCategoria> ObtenerTodos()
        {
            return _contexto.SubCategorias.ToList();
        }
        public List<SubCategoria> ObtenerTodas()
        {
            return _contexto.SubCategorias.ToList();
        }
    }
}
