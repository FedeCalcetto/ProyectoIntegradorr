using ProyectoIntegrador.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Interface
{
    public interface IObtenerProductosDeInteres
    {
        List<Producto> Obtener(int usuarioId, int maxItems = 15);

    }
}
