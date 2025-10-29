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
        public void Agregar(Factura entidad)
        {
            throw new NotImplementedException();
        }

        public void Editar(Factura entidad)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Factura Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Factura> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
