using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Interface
{
    public interface IEmailService
    {
        Task EnviarAvisoPedidoAceptadoAsync(
        string destino,
        string nombreArtesano,
        string telefonoArtesano,
        string tituloPedido
        );

        Task EnviarAvisoPedidoFinalizadoAsync(
        string destino,
        string tituloPedido,
        string nombreArtesano,
        string emailArtesano
        );
    }
}
