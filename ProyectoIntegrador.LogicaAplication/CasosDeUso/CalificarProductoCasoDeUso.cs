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
    public class CalificarProductoCasoDeUso : ICalificarProducto
    {
        private readonly ICalificarProductoRepositorio _calificarProductoRepo;

        public CalificarProductoCasoDeUso(ICalificarProductoRepositorio calificarProductoRepo)
        {
            _calificarProductoRepo = calificarProductoRepo;
        }
        public async Task Ejecutar(int productoId, decimal puntaje, int usuarioId)
        {
            if (puntaje < 0 || puntaje > 5)
                throw new Exception("Puntaje inválido");

            // verificar si ya calificó
            var existente = await _calificarProductoRepo.ObtenerPorUsuarioYProducto(usuarioId, productoId);

            if (existente != null)
            {
                existente.puntaje = puntaje;
                _calificarProductoRepo.Actualizar(existente);
            }
            else
                _calificarProductoRepo.Agregar(productoId, usuarioId, puntaje);
        }
    }
}
