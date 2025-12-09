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
    public class ObtenerProductoCasoDeUso : IObtenerProducto
    {

        private readonly IProductoRepositorio _productoRepo;

        public ObtenerProductoCasoDeUso(IProductoRepositorio productoRepo)
        {
            _productoRepo = productoRepo;
        }


        public Producto obtener(int id)
        {
            return _productoRepo.Obtener(id);
        }
    }
}
