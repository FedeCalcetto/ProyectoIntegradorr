using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Interface
{
    public interface IEliminarItemDelCarrito
    {
        void Eliminar(int carritoItemId, int cantidadABorrar);
    }
}
