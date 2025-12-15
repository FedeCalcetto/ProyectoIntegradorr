using ProyectoIntegrador.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Interface
{
    public interface IProductosFiltrados
    {
        List<Producto> Ejecutar(string filtro, int? precioMin, int? precioMax, int pagina, int tamanoPagina, out int totalRegistros);
    }
}
