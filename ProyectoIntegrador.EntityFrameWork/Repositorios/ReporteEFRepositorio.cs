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
    public class ReporteEFRepositorio : IReporteRepositorio
    {
        private readonly ProyectoDBContext _contexto;

        public ReporteEFRepositorio(ProyectoDBContext dbContext)
        {
            _contexto = dbContext;
        }

        public void Agregar(Reporte entidad)
        {
            entidad.Validar();
            _contexto.Reportes.Add(entidad);
            _contexto.SaveChanges();
        }

        public void Editar(Reporte entidad)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            var productoDominio = ObtenerReporte(id);

            if (productoDominio is null)
            {
                throw new ProductoNoEncontradoException();
            }

            _contexto.Reportes.Remove(productoDominio);
            _contexto.SaveChanges();
        }

        public Reporte Obtener(int id)
        {
            return _contexto.Reportes
            .Include(r => r.producto)
            .Include(r => r.artesano)
            .Include(r => r.cliente)
            .FirstOrDefault(r => r.id == id);
        }

        public IEnumerable<Reporte> ObtenerTodos()
        {
            return _contexto.Reportes.Include(r => r.artesano).Include(r => r.producto).Include(r => r.cliente).ToList();
        }
        public Reporte ObtenerReporte(int id)
        {
            return _contexto.Reportes
            .FirstOrDefault(r => r.id == id);
        }

        public Boolean ArtesanoConReportes(int artesanoId)
        {

            return _contexto.Reportes.Any(r => r.artesanoId == artesanoId);
        }
    }
}
