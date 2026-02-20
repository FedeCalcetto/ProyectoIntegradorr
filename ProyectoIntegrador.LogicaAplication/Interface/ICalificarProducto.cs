using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Interface
{
    public interface ICalificarProducto
    {
        Task Ejecutar(int productoId, decimal puntaje, int usuarioId);
    }
}
