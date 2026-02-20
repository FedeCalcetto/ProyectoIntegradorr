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
            entidad.password = artesano.password;
            entidad.descripcion = artesano.descripcion;

            entidad.telefono = artesano.telefono;
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
            var artesano = _contexto.Usuarios
                .OfType<Artesano>()
                .Include(a => a.productos)
                .FirstOrDefault(a => a.id == id);

            if (artesano == null)
                throw new Exception("Artesano no encontrado");

            // 1) Eliminar productos primero
           /* foreach (var prod in artesano.productos.ToList())
            {
                _contexto.Productos.Remove(prod);
            }*/

            // 2) Luego eliminar el artesano
            _contexto.Usuarios.Remove(artesano);

            _contexto.SaveChanges();
        }

        public Artesano Obtener(int id)
        {
            return _contexto.Usuarios
                       .OfType<Artesano>()
                       .Include(a => a.productos)
                       .Include(a => a.comentarios)
                       .ThenInclude(c => c.cliente)
                       .FirstOrDefault(a => a.id == id);
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
            .ThenInclude(p => p.Fotos)
        .FirstOrDefault(a => a.email.email == email);
        }

        public IEnumerable<Artesano> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public void bloquearArtesano(int id)
        {
            var artesano = _contexto.Artesanos.FirstOrDefault(a => a.id == id);

            if (artesano is null)
            {
                throw new ClienteNoEncontradoException();
            }
            artesano.bloqueado = true;
            _contexto.SaveChanges();
        }

        public Artesano ObtenerArtesanoDashboard(string email)
        {
            return _contexto.Artesanos
           .Include(a => a.productos)
           .Include(a => a.ventas)
           .ThenInclude(v => v.Orden)
            .ThenInclude(o => o.Items)
           .FirstOrDefault(a => a.email.email == email);

        }
    }


}
