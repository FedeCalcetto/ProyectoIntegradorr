using ProyectoIntegrador.LogicaNegocio.Entidades;
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
            throw new NotImplementedException();
        }

        public Reporte Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reporte> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
