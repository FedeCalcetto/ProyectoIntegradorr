using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.EntityFrameWork.Repositorios
{
    public class ArtesanoEFRepositorio : IArtesanoRepositorio
    {
        private readonly ProyectoDBContext _contexto;

        public ArtesanoEFRepositorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }


        public void Agregar(Artesano entidad)
        {
            throw new NotImplementedException();
        }

        public void Editar(Artesano entidad)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Artesano Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Artesano> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
