using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.EntityFrameWork.Repositorios
{
    public class PedidoPersonalizadoEFRepositorio : IPedidoPersonalizadoRepsoitorio
    {
        private readonly ProyectoDBContext _contexto;

        public PedidoPersonalizadoEFRepositorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }


        public void Agregar(PedidoPersonalizado entidad)
        {
            throw new NotImplementedException();
        }

        public void Editar(PedidoPersonalizado entidad)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public PedidoPersonalizado Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PedidoPersonalizado> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
