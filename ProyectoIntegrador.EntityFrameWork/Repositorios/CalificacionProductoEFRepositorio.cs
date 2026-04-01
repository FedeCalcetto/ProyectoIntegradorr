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
    public class CalificacionProductoEFRepositorio : ICalificarProductoRepositorio
    {
        private readonly ProyectoDBContext _contexto;

        public CalificacionProductoEFRepositorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }

        public void Actualizar(CalificacionProducto calificacion)
        {
            _contexto.CalificacionesProducto.Update(calificacion);
            _contexto.SaveChanges();
        }

        public void Agregar(int productoId, int usuarioId, decimal puntaje)
        {
            var ca = CalificacionProducto.ParaProducto(productoId, usuarioId, puntaje);
            _contexto.CalificacionesProducto.Add(ca);
            _contexto.SaveChanges();
        }

        public async Task<CalificacionProducto?> ObtenerPorUsuarioYProducto(int usuarioId, int productoId)
        {
            return await _contexto.CalificacionesProducto
                .FirstOrDefaultAsync(c => c.productoId == productoId && c.usuarioId == usuarioId);
        }

        public decimal ObtenerPromedioPorProducto(int productoId)
        {
            var query = _contexto.CalificacionesProducto
                .Where(c => c.productoId == productoId);

            if (!query.Any())
                return 0;

            return query.Average(c => c.puntaje);
        }

        public int ObtenerTotalCalificaciones(int productoId)
        {
            return _contexto.CalificacionesProducto
                .Count(c => c.productoId == productoId);
        }

        public void EliminarParaProducto(int productoId)
        {
            var query = _contexto.CalificacionesProducto
                .Where(c => c.productoId == productoId)
                .ToList();

            _contexto.CalificacionesProducto.RemoveRange(query);
            _contexto.SaveChanges();
        }
    }
}