using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.EntityFrameWork.Repositorios
{
    public class ProductoEFRepositorio : IProductoRepositorio
    {

        private readonly ProyectoDBContext _contexto;

        public ProductoEFRepositorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }
        public void Agregar(Producto entidad)
        {
            throw new NotImplementedException();
        }

        public void Editar(Producto entidad)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Producto Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Producto> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
