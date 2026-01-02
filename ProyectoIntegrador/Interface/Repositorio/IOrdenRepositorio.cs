using ProyectoIntegrador.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Interface.Repositorio
{
    public interface IOrdenRepositorio
    {
        Task CrearOrdenAsync(Orden orden);
        Task<Orden?> ObtenerOrdenPorIdAsync(Guid ordenId);
        Task ActualizarOrdenAsync(Orden orden);
    }
}
