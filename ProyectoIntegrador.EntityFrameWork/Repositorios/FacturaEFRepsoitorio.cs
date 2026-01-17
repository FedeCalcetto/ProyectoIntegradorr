using Microsoft.EntityFrameworkCore;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.EntityFrameWork.Repositorios
{
    public class FacturaEFRepsoitorio : IFacturaRepositorio
    {

        private readonly ProyectoDBContext _contexto;

        public FacturaEFRepsoitorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }
        public void Agregar(FacturaNoFiscal entidad)
        {
            _contexto.Add(entidad);
            _contexto.SaveChanges();
        }

        public void CrearFacturas(Orden o)
        {
                if (ExisteFacturaParaOrden(o.Id))
                    return;

                var facturaCliente = new FacturaNoFiscalCliente
                {
                    ClienteId = o.ClienteId,
                    OrdenId = o.Id,
                    Fecha = DateTime.Now,
                    Total = o.Total,
                    itemsFactura = o.Items.Select(i => new LineaFactura
                    {
                        idProducto = i.ProductoId,
                        precioUnitario = (int)i.PrecioUnitario,
                        cantidad = i.Cantidad
                    }).ToList()
                };
                _contexto.Add(facturaCliente);


                var itemsPorArtesano = o.Items
                .GroupBy(i => i.ArtesanoId);

                foreach (var grupo in itemsPorArtesano)
                {
                    var facturaArtesano = new FacturaNoFiscalArtesano
                    {
                        ArtesanoId = grupo.Key,
                        OrdenId = o.Id,
                        Fecha = DateTime.Now,
                        Total = grupo.Sum(i => i.PrecioUnitario * i.Cantidad),
                        itemsFactura = grupo.Select(i => new LineaFactura
                        {
                            idProducto = i.ProductoId,
                            precioUnitario = (int)i.PrecioUnitario,
                            cantidad = i.Cantidad
                        }).ToList()
                    };
                    _contexto.Add(facturaArtesano);
                }        
            _contexto.SaveChanges();
            }
        

        public void Editar(FacturaNoFiscal entidad)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public bool ExisteFacturaParaOrden(Guid ordenId)
        {
            return _contexto.Facturas
                      .Any(f => f.OrdenId == ordenId);
        }

        public FacturaNoFiscal Obtener(int id)
        {
           return _contexto.Facturas.FirstOrDefault(f => f.Id == id);
        }

        public FacturaNoFiscalCliente ObtenerFacturaCliente(int id)
        {
            return _contexto.Facturas
         .OfType<FacturaNoFiscalCliente>()
         .Include(f => f.Cliente)
         .Include(f => f.itemsFactura)
             .ThenInclude(i => i.producto)
                 .ThenInclude(p => p.artesano)
         .FirstOrDefault(f => f.Id == id);
        }

        public FacturaNoFiscalCliente ObtenerFacturaClientePorOrden(Guid ordenId)
        {
            return _contexto.Facturas
                   .OfType<FacturaNoFiscalCliente>()  
                   .Include(f => f.Cliente)
                   .Include(f => f.itemsFactura)
                       .ThenInclude(lf => lf.producto)
                           .ThenInclude(p => p.artesano)
                   .FirstOrDefault(f => f.OrdenId == ordenId);
        }

        public IEnumerable<FacturaNoFiscalArtesano> ObtenerPorArtesano(string? email)
        {
            return _contexto.Facturas
             .OfType<FacturaNoFiscalArtesano>()
             .Include(f => f.Artesano)
             .Where(f => f.Artesano.email.email == email)
             .OrderByDescending(f => f.Fecha)
             .ToList();
        }

        public IEnumerable<FacturaNoFiscalCliente> ObtenerPorCliente(string? email)
        {
            return _contexto.Facturas
            .OfType<FacturaNoFiscalCliente>()
            .Include(f => f.Cliente)
            .Where(f => f.Cliente.email.email == email)
            .OrderByDescending(f => f.Fecha)
            .ToList();
        }

        public IEnumerable<FacturaNoFiscal> ObtenerTodos()
        {
            return _contexto.Facturas.ToList();
        }
    }
}
