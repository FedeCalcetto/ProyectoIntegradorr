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
    public class ObtenerCalificacionProductoCasoDeUso : IObtenerPromedioCalificacionDeProducto
    {
        private readonly ICalificarProductoRepositorio _calificarProductoRepo;

        public ObtenerCalificacionProductoCasoDeUso(ICalificarProductoRepositorio calificarProductoRepo)
        {
            _calificarProductoRepo = calificarProductoRepo;
        }
        public decimal ObtenerPromedioPorProducto(int productoId)
        {
            return _calificarProductoRepo
        .ObtenerPromedioPorProducto(productoId);
        }

        public int ObtenerTodasLasCalificaciones(int productoId)
        {
            return _calificarProductoRepo.ObtenerTotalCalificaciones(productoId);
        }
    }
}
