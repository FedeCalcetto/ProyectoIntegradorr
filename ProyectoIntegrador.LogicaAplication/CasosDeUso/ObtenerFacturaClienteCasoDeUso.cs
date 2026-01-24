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
    public class ObtenerFacturaClienteCasoDeUso : IObtenerFacturaCliente
    {
        private readonly IFacturaRepositorio _facturaRepo;

        public ObtenerFacturaClienteCasoDeUso(IFacturaRepositorio facturaRepo)
        {
            _facturaRepo = facturaRepo;
        }
        public FacturaNoFiscalCliente obtenerFacturaCliente(int id)
        {
            return _facturaRepo.ObtenerFacturaCliente(id);
        }
    }
}
