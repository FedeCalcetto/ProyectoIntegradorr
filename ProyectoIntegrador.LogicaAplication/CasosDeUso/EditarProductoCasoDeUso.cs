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
    public class EditarProductoCasoDeUso : IEditarProducto
    {

        private readonly IProductoRepositorio _productoRepositorio;

        public EditarProductoCasoDeUso(IProductoRepositorio productoRepositorio)
        {
            _productoRepositorio = productoRepositorio;
        }

        public void Ejecutar(Producto entidad)
        {
            _productoRepositorio.Editar(entidad);
        }
    }
}
