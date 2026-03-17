using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class CalificarArtesanoCasoDeUso : ICalificarArtesano
    {

        private readonly ICalificarArtesanoRepositorio _calificarRepo;

        public CalificarArtesanoCasoDeUso(ICalificarArtesanoRepositorio calificarRepo)
        {
            _calificarRepo = calificarRepo;
        }

        public async Task Ejecutar(int arteId, decimal puntaje, int usuarioId)
        {
            if (puntaje < 0 || puntaje > 5)
                throw new Exception("Puntaje inválido");

            // verificar si ya calificó
            var existente = await _calificarRepo.ObtenerPorUsuarioYArtesano(usuarioId, arteId);

            if (existente != null)
            {
                existente.puntaje = puntaje;
                _calificarRepo.Actualizar(existente);
            }
            else
                _calificarRepo.Agregar(arteId, usuarioId, puntaje);
        }
    }
}
