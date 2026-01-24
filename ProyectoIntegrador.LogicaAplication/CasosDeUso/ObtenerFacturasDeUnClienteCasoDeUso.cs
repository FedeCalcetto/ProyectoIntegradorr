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
    public class ObtenerFacturasDeUnClienteCasoDeUso : IObtenerFacturasDeUnCliente
    {
        private readonly IFacturaRepositorio _facturaRepo;

        public ObtenerFacturasDeUnClienteCasoDeUso(IFacturaRepositorio facturaRepo)
        {
            _facturaRepo = facturaRepo;
        }

        public IEnumerable<FacturaNoFiscalCliente> obtenerFacturasDeUnCliente(string email)
        {
            return _facturaRepo.ObtenerPorCliente(email);
        }
    }
}
