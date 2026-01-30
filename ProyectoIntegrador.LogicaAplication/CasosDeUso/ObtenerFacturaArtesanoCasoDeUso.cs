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
    public class ObtenerFacturaArtesanoCasoDeUso : IObtenerFacturaArtesano
    {
        private readonly IFacturaRepositorio _facturaRepo;

        public ObtenerFacturaArtesanoCasoDeUso(IFacturaRepositorio facturaRepo)
        {
            _facturaRepo = facturaRepo;
        }
        public FacturaNoFiscalArtesano obtenerFacturaArteasano(int facturaId)
        {
            return _facturaRepo.ObtenerFacturaArtesano(facturaId);
        }

        public List<FacturaNoFiscalArtesano> obtenerTodasLasFacturasDelArtesano(int id)
        {
            return _facturaRepo.ObtenerFacturasArtesano(id);
        }
    }
}
