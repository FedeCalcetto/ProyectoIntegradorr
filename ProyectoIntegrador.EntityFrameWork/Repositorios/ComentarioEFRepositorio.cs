using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.EntityFrameWork.Repositorios
{
    public class ComentarioEFRepositorio : IComentarioRepositorio
    {

        private readonly ProyectoDBContext _contexto;

        public ComentarioEFRepositorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }

        public void Agregar(Comentario entidad)
        {
            entidad.Validar();
            _contexto.Comentarios.Add(entidad);
            _contexto.SaveChanges();
        }

        public void Editar(Comentario entidad)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Comentario Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comentario> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
        public void EliminarPorArtesano(int artesanoId)
        {
            var comentarios = _contexto.Comentarios
                .Where(c => c.artesanoId == artesanoId)
                .ToList();

            _contexto.Comentarios.RemoveRange(comentarios);
            _contexto.SaveChanges();
        }
    }
}
