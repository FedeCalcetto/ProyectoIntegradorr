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
    public class ObtenerFacturaClientePorOrdenCasoDeUso : IObtenerFacturaClientePorOrden
    {
        private readonly IFacturaRepositorio _facturaRepo;

        public ObtenerFacturaClientePorOrdenCasoDeUso(IFacturaRepositorio facturaRepo)
        {
            _facturaRepo = facturaRepo;
        }

        public FacturaNoFiscalCliente obtenerFacturaClientePorOrden(Guid ordenId)
        {
           return _facturaRepo.ObtenerFacturaClientePorOrden(ordenId);
        }
    }
}
