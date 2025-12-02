using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.EntityFrameWork.Repositorios
{
    public class ProductoFotoEFRepositorio : IProductoFotoRepsoitorio
    {
        private readonly ProyectoDBContext _contexto;

        public ProductoFotoEFRepositorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }

        public void Agregar(ProductoFoto entidad)
        {
            _contexto.ProductoFotos.Add(entidad);
            _contexto.SaveChanges();
        }

        public void Editar(ProductoFoto entidad)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public ProductoFoto Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductoFoto> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
