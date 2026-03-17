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
    public class CalificacionEFRepositorio : ICalificarProducto2Repositorio
    {
        private readonly ProyectoDBContext _contexto;

        public CalificacionEFRepositorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }

        public void Actualizar(Calificación calificacion)
        {
            _contexto.Calificaciones.Update(calificacion);
             _contexto.SaveChanges();
        }

        public void Agregar(int productoId, int usuarioId, decimal puntaje)
        {
            var ca = Calificación.ParaProducto(productoId, usuarioId, puntaje);
            _contexto.Calificaciones.Add(ca);
            _contexto.SaveChanges();

        }

        public void AgregarParaArtesano(int arteId, int usuarioId, decimal puntaje)
        {
            var ca = Calificación.ParaArtesano(arteId, usuarioId, puntaje);
            _contexto.Calificaciones.Add(ca);
            _contexto.SaveChanges();
        }

        public async Task<Calificación?> ObtenerPorUsuarioYProducto(int usuarioId, int productoId)
        {
            return await _contexto.Calificaciones.
                FirstOrDefaultAsync(ca => ca.productoId == productoId && ca.usuarioId == usuarioId);
        }

        public async Task<Calificación?> ObtenerPorUsuarioYArtesano(int usuarioId, int arteId)
        {
            return await _contexto.Calificaciones.
                FirstOrDefaultAsync(ca => ca.artesanoId == arteId && ca.usuarioId == usuarioId);
        }

        public decimal ObtenerPromedioDeUnProducto(int productoId)
        {
            throw new NotImplementedException();
        }

        public decimal ObtenerPromedioPorProducto(int productoId)
        {
            var query = _contexto.Calificaciones
            .Where(c => c.productoId == productoId);

            if (!query.Any())
                return 0;

            return query.Average(c => c.puntaje);
        }

        public decimal ObtenerPromedioPorArtesano(int arteId)
        {
            var query = _contexto.Calificaciones
            .Where(c => c.artesanoId == arteId);

            if (!query.Any())
                return 0;

            return query.Average(c => c.puntaje);
        }

        public int  ObtenerTotalCalificaciones(int productoId)
        {
            return _contexto.Calificaciones
        .Count(c => c.productoId == productoId);
        }
        public int ObtenerTotalCalificacionesArtesano(int arteId)
        {
            return _contexto.Calificaciones
        .Count(c => c.artesanoId == arteId);
        }
        public void EliminarParaProudcto(int id)
        {
            var query = _contexto.Calificaciones
            .Where(c => c.productoId == id).ToList();

            _contexto.Calificaciones.RemoveRange(query);
            _contexto.SaveChanges();
        }

        public void EliminarParaArtesano(int id)
        {
            var query = _contexto.Calificaciones
            .Where(c => c.artesanoId == id).ToList();

            _contexto.Calificaciones.RemoveRange(query);
            _contexto.SaveChanges();
        }

    }
}
