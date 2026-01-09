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
    public class OrdenEFRepositorio : IOrdenRepositorio
    {

        private readonly ProyectoDBContext _contexto;

        public OrdenEFRepositorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }
        public async Task ActualizarOrdenAsync(Orden orden) //Task -> acción que terminará en el futuro, se usa await y async para no detener el flujo mientras se realiza la acción
        {
            _contexto.Ordenes.Update(orden);
            await _contexto.SaveChangesAsync();

        }

        public async Task CrearOrdenAsync(Orden orden)
        {
            _contexto.Add(orden);
            await _contexto.SaveChangesAsync();
        }

        public async Task<Orden?> ObtenerOrdenPorIdAsync(Guid ordenId)
        {
            return await _contexto.Ordenes
            .Include(o => o.Items)
            .Include(o => o.Artesano)
            .FirstOrDefaultAsync(o => o.Id == ordenId);
        }

        public async Task<List<Orden>> ObtenerPorIdsAsync(List<Guid> ids)
        {
               return await _contexto.Ordenes
              .Include(o => o.Artesano)
              .Where(o => ids.Contains(o.Id))
              .ToListAsync();
        }

    }
}
