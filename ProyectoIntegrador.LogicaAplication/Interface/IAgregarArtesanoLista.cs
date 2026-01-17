using ProyectoIntegrador.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Interface
{
    public interface IAgregarArtesanoLista
    {
        void agregarArtesano(Cliente c, Artesano a);
    }
}
