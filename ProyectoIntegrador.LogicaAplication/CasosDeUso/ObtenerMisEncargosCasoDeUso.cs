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
    public class ObtenerMisEncargosCasoDeUso : IObtenerMisEncargos
    {
        private readonly IPedidoPersonalizadoRepsoitorio _repo;

        public ObtenerMisEncargosCasoDeUso(IPedidoPersonalizadoRepsoitorio repo)
        {
            _repo = repo;
        }

        public IEnumerable<PedidoPersonalizado> Ejecutar(string emailArtesano)
        {
            return _repo.ObtenerPorArtesano(emailArtesano);
        }
    }
}
