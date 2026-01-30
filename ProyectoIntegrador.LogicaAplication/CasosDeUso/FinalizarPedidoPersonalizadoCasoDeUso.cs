using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Excepciones;
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
        private readonly IEmailService _emailService;

        public FinalizarPedidoPersonalizadoCasoDeUso(IPedidoPersonalizadoRepsoitorio repo, IEmailService emailService)
        {
            _repo = repo;
            _emailService = emailService;
        }

        public async Task Ejecutar(int pedidoId)
        {
            var pedido = _repo.Obtener(pedidoId);
            pedido.Estado = EstadoPedido.Finalizado;
            pedido.FechaFinalizacion = DateTime.Now;

            _repo.Editar(pedido);

            await _emailService.EnviarAvisoPedidoFinalizadoAsync(pedido.Cliente.email.email, pedido.Titulo, $"{pedido.Artesano.nombre} {pedido.Artesano.apellido}", pedido.Artesano.email?.email);
        }
    }

}
