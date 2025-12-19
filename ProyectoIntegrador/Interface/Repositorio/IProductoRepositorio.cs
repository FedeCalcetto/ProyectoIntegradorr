using ProyectoIntegrador.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Interface.Repositorio
{
    public interface IProductoRepositorio : IRepositorio<Producto>
    {
        void Editar(Producto producto, List<string> fotos);
        List<Producto> ObtenerProductosExcluyendo(List<int> idsEnCarrito, int maxItems);
        IEnumerable<Producto> ObtenerPublicados();
        IEnumerable<Producto> ObtenerPublicadosPorSubCategoria(int subCategoriaId);
        List<Producto> ProductosFiltrados(string filtro,int? precioMin, int? precioMax, int pagina, int totalPagina, out int totalRegistros, int? categoriaId, int? subCategoriaId);

    }
}
