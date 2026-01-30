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
        private readonly IEmailService _emailService;

        public AceptarPedidoPersonalizadoCasoDeUso(
            IPedidoPersonalizadoRepsoitorio pedidoRepo,
            IArtesanoRepositorio artesanoRepo,
            IEmailService emailService)
        {
            _pedidoRepo = pedidoRepo;
            _artesanoRepo = artesanoRepo;
            _emailService = emailService;
        }

        public async Task Ejecutar(int pedidoId, string emailArtesano)
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

            ///// para pasar nombre en un valor /////
            var nombreArtesano = $"{artesano.nombre} {artesano.apellido}";
            var emailContacto = artesano.email?.email;

            //Console.WriteLine("EMAIL RAW: " + artesano.email);
            //Console.WriteLine("EMAIL.VALUE: " + artesano.email?.email);

            var telefono = string.IsNullOrWhiteSpace(artesano.telefono)
            ? $"Este artesano aún no proporcionó su teléfono. Podés contactarlo al mail: {emailContacto}"
            : artesano.telefono;
            ///////////////////////////////////////////////////
            
            await _emailService.EnviarAvisoPedidoAceptadoAsync(
            pedido.Cliente.email.email,
            $"{artesano.nombre} {artesano.apellido}",
            telefono,
            pedido.Titulo
            );
        }

       
    }
}
