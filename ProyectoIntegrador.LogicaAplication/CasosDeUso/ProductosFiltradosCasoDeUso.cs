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
    public class ProductosFiltradosCasoDeUso : IProductosFiltrados
    {
     
        private readonly IProductoRepositorio _productoRepositorio;

        public ProductosFiltradosCasoDeUso(IProductoRepositorio productoRepositorio)
        {
            _productoRepositorio = productoRepositorio;
        }
        
        public List<Producto> Ejecutar(string filtro,int? precioMin, int? precioMax, int pagina, int totalPagina, out int totalRegistros, int? categoriaId, int? subCategoriaId)
        {
            return _productoRepositorio.ProductosFiltrados(filtro,precioMin, precioMax,pagina,totalPagina, out totalRegistros,categoriaId,subCategoriaId);
        }
    }
}
