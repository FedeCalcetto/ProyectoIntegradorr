using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.EntityFrameWork.Repositorios
{
    public class FacturaEFRepsoitorio : IFacturaRepositorio
    {

        private readonly ProyectoDBContext _contexto;

        public FacturaEFRepsoitorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }
        public void Agregar(FacturaNoFiscal entidad)
        {
            throw new NotImplementedException();
        }

        public void Editar(FacturaNoFiscal entidad)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public FacturaNoFiscal Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FacturaNoFiscal> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
