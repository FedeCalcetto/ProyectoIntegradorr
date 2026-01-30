using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class ObtenerOrdenCasoDeUso: IObtenerOrden
    {
        private readonly IOrdenRepositorio _ordenRepo;

        public ObtenerOrdenCasoDeUso(
            IOrdenRepositorio ordenRepo)
        {
            _ordenRepo = ordenRepo;
        }

        public async Task<Orden?> ObtenerOrdenPorIdAsync(Guid ordenId)
        {
            return await _ordenRepo.ObtenerOrdenPorIdAsync(ordenId);
        }
    }
}
