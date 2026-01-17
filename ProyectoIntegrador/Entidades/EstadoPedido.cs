using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public enum EstadoPedido
    {
        Pendiente,   // creado por cliente, visible para todos
        Aceptado,    // tomado por un artesano
        Finalizado   // terminado
    }
}
