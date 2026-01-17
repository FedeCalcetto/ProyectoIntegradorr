using ProyectoIntegrador.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Interface.Repositorio
{
    public interface IFacturaRepositorio : IRepositorio<FacturaNoFiscal>
    {
        void CrearFacturas(Orden o);
        bool ExisteFacturaParaOrden(Guid ordenId);
        FacturaNoFiscalCliente ObtenerFacturaCliente(int id);
        FacturaNoFiscalCliente ObtenerFacturaClientePorOrden(Guid ordenId);
        IEnumerable<FacturaNoFiscalArtesano> ObtenerPorArtesano(string? email);
        IEnumerable<FacturaNoFiscalCliente> ObtenerPorCliente(string? email);
    }
}
