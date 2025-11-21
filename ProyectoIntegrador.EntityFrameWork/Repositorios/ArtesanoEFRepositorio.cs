using Microsoft.EntityFrameworkCore;
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
    public class ArtesanoEFRepositorio : IArtesanoRepositorio
    {
        private readonly ProyectoDBContext _contexto;

        public ArtesanoEFRepositorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }

        public void Actualizar(Artesano artesano)
        {

            var entidad = ObtenerPorEmail(artesano.email.email);

            if (entidad is null)
            {
                throw new ClienteNoEncontradoException();
            }
            entidad.foto = artesano.foto;
            entidad.nombre = artesano.nombre;
            entidad.apellido = artesano.apellido;
            //entidad.email = cliente.email;
            entidad.password = artesano.password;
            _contexto.Update(entidad);
            _contexto.SaveChanges();
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

        public Artesano ObtenerPorEmail(string email) {

            return _contexto.Usuarios
                      .OfType<Artesano>()
                      .FirstOrDefault(a => a.email.email == email);

        }

        public Artesano ObtenerProductosArtesano(string email)
        {
            return _contexto.Usuarios
            .OfType<Artesano>()
            .Include(a => a.productos)
            .FirstOrDefault(a => a.email.email == email);
        }

        public IEnumerable<Artesano> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
