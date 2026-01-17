using ProyectoIntegrador.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Interface
{
    public interface IAgregarOrden
    {
        Task<List<Guid>> AgregarOrdenesAsync(int usuarioId);
        Task<Guid> AgregarOrdenAsync(int usuarioId);

    }
}
