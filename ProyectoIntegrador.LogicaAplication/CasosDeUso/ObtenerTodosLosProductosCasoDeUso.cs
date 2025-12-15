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
    public class ObtenerTodosLosProductosCasoDeUso : IObtenerTodosLosProductos
    {
        private readonly IProductoRepositorio _productoRepo;

        public ObtenerTodosLosProductosCasoDeUso(IProductoRepositorio productoRepo)
        {
            _productoRepo = productoRepo;
        }
        public IEnumerable<Producto> obtenerTodos()
        {
            return _productoRepo.ObtenerTodos();
        }
    }
}
