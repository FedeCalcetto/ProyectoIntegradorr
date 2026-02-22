using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Interface
{
    public interface IToggleFavorito
    {
        bool Ejecutar(string emailCliente, int productoId);
    }
}
