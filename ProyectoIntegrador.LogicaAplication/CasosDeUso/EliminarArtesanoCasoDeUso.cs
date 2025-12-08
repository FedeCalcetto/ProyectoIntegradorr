using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class EliminarArtesanoCasoDeUso: IEliminarArtesano
    {
        private readonly IArtesanoRepositorio _repoArtesano;
        private readonly IObtenerArtesano _obtenerArtesano;

        public EliminarArtesanoCasoDeUso(IArtesanoRepositorio repoArtesano,
                                         IObtenerArtesano obtenerArtesano)
        {
            _repoArtesano = repoArtesano;
            _obtenerArtesano = obtenerArtesano;
        }

        public void Ejecutar(string email)
        {
            var artesano = _obtenerArtesano.Ejecutar(email);

            if (artesano == null)
                throw new Exception("El artesano no existe.");

            _repoArtesano.Eliminar(artesano.id);
        }

    }
}
