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
    public class ObtenerProductosDeInteresCasoDeUso : IObtenerProductosDeInteres
    {
        private readonly IProductoRepositorio _productoRepo;
        private readonly ICarritoRepositorio _carritoRepo;
    public ObtenerProductosDeInteresCasoDeUso(IProductoRepositorio productoRepo, ICarritoRepositorio carritoRepo)

    {
        _productoRepo = productoRepo;
        _carritoRepo = carritoRepo;
    }

    public List<Producto> Obtener(int usuarioId, int maxItems = 15)
        {
            var idsEnCarrito = _carritoRepo
                .ObtenerItemsDeUsuario(usuarioId)
                .Select(ci => ci.producto.id)
                .ToList();

            return _productoRepo
                .ObtenerProductosExcluyendo(idsEnCarrito, maxItems);
        }
    }
}

