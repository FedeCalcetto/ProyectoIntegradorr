using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class FinalizarPedidoPersonalizadoCasoDeUso : IFinalizarPedidoPersonalizado
    {
        private readonly IPedidoPersonalizadoRepsoitorio _repo;

        public FinalizarPedidoPersonalizadoCasoDeUso(IPedidoPersonalizadoRepsoitorio repo)
        {
            _repo = repo;
        }

        public void Ejecutar(int pedidoId)
        {
            var pedido = _repo.Obtener(pedidoId);

            pedido.Estado = EstadoPedido.Finalizado;
            pedido.FechaFinalizacion = DateTime.Now;

            _repo.Editar(pedido);
        }
    }

}
