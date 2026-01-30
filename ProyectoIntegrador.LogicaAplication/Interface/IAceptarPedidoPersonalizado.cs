using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Interface
{
    public interface IAceptarPedidoPersonalizado
    {
        Task Ejecutar(int pedidoId, string emailArtesano);
    }

}
