using ProyectoIntegrador.LogicaNegocio.Entidades;

namespace ProyectoIntegrador_Web.Services
{
    public interface IProcesarOrdenService
    {
        Task ProcesarOrdenPagadaAsync(Orden orden, long paymentId);
        Task ProcesarOrdenRechazadaAsync(Orden orden);
        Task ProcesarOrdenCanceladaAsync(Orden orden);
        Task ProcesarOrdenPendienteAsync(Orden orden);
    }
}
