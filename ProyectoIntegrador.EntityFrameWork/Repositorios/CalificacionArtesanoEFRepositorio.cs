using Microsoft.EntityFrameworkCore;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.EntityFrameWork.Repositorios
{
    public class CalificacionArtesanoEFRepositorio : ICalificarArtesanoRepositorio
    {
        private readonly ProyectoDBContext _contexto;

        public CalificacionArtesanoEFRepositorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }

        public void Actualizar(CalificacionArtesano calificacion)
        {
            _contexto.CalificacionesArtesano.Update(calificacion);
            _contexto.SaveChanges();
        }

        public void Agregar(int artesanoId, int usuarioId, decimal puntaje)
        {
            var ca = CalificacionArtesano.ParaArtesano(artesanoId, usuarioId, puntaje);
            _contexto.CalificacionesArtesano.Add(ca);
            _contexto.SaveChanges();
        }

        public async Task<CalificacionArtesano?> ObtenerPorUsuarioYArtesano(int usuarioId, int artesanoId)
        {
            return await _contexto.CalificacionesArtesano
                .FirstOrDefaultAsync(c => c.artesanoId == artesanoId && c.usuarioId == usuarioId);
        }

        public decimal ObtenerPromedioPorArtesano(int artesanoId)
        {
            var productosIds = _contexto.Productos
                .Where(p => p.ArtesanoId == artesanoId)
                .Select(p => p.id)
                .ToList();

            var calificaciones = _contexto.CalificacionesProducto
                .Where(c => productosIds.Contains(c.productoId));

            if (!calificaciones.Any())
                return 0;

            return calificaciones.Average(c => c.puntaje);
        }

        public int ObtenerTotalCalificaciones(int artesanoId)
        {
                    var productosIds = _contexto.Productos
                 .Where(p => p.ArtesanoId == artesanoId)
                 .Select(p => p.id)
                 .ToList();

            return _contexto.CalificacionesProducto
                .Count(c => productosIds.Contains(c.productoId));
        }

        public void EliminarParaArtesano(int artesanoId)
        {
            var query = _contexto.CalificacionesArtesano
                .Where(c => c.artesanoId == artesanoId)
                .ToList();

            _contexto.CalificacionesArtesano.RemoveRange(query);
            _contexto.SaveChanges();
        }
    }
}