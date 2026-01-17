using ProyectoIntegrador.LogicaAplication.Dtos;
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
    public class CrearPedidoPersonalizadoCasoDeUso : ICrearPedidoPersonalizado
    {
        private readonly IPedidoPersonalizadoRepsoitorio _pedidoRepo;
        private readonly IClienteRepositorio _clienteRepo;

        public CrearPedidoPersonalizadoCasoDeUso(
            IPedidoPersonalizadoRepsoitorio pedidoRepo,
            IClienteRepositorio clienteRepo)
        {
            _pedidoRepo = pedidoRepo;
            _clienteRepo = clienteRepo;
        }

        public PedidoPersonalizado Ejecutar(CrearPedidoDto dto)
        {
            var cliente = _clienteRepo.obtenerCliente(dto.EmailCliente);

            if (cliente == null)
                throw new ClienteNoEncontradoException();

            var pedido = new PedidoPersonalizado
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                ClienteId = cliente.id,
                Estado = EstadoPedido.Pendiente,
                FechaCreacion = DateTime.Now
            };

            _pedidoRepo.Agregar(pedido);

            return pedido;
        }
    }
}
