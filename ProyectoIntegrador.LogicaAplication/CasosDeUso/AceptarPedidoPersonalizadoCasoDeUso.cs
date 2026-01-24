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
    public class AceptarPedidoPersonalizadoCasoDeUso : IAceptarPedidoPersonalizado
    {
        private readonly IPedidoPersonalizadoRepsoitorio _pedidoRepo;
        private readonly IArtesanoRepositorio _artesanoRepo;

        public AceptarPedidoPersonalizadoCasoDeUso(
            IPedidoPersonalizadoRepsoitorio pedidoRepo,
            IArtesanoRepositorio artesanoRepo)
        {
            _pedidoRepo = pedidoRepo;
            _artesanoRepo = artesanoRepo;
        }

        public void Ejecutar(int pedidoId, string emailArtesano)
        {
            var pedido = _pedidoRepo.Obtener(pedidoId);

            if (pedido == null)
                throw new Exception("Pedido no encontrado");

            if (pedido.Estado != EstadoPedido.Pendiente)
                throw new PedidoYaAceptadoException();

            var artesano = _artesanoRepo.ObtenerPorEmail(emailArtesano);

            pedido.ArtesanoId = artesano.id;
            pedido.Estado = EstadoPedido.Aceptado;

            _pedidoRepo.Editar(pedido);
        }
    }
}
