using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class EliminarProductoCasoDeUso : IEliminarProducto
    {

        private readonly IProductoRepositorio _productoRepositorio;

        public EliminarProductoCasoDeUso(IProductoRepositorio productoRepositorio)
        {
            _productoRepositorio = productoRepositorio;
        }

        public void Ejecutar(int id)
        {
            _productoRepositorio.Eliminar(id);
        }
    }
}
