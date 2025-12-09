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

    }
}
