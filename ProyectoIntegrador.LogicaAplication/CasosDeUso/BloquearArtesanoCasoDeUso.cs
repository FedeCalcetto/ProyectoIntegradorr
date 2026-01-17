using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class BloquearArtesanoCasoDeUso :IBloquearArtesano
    {
        private readonly IArtesanoRepositorio _artesanoRepo;

        public BloquearArtesanoCasoDeUso(IArtesanoRepositorio artesanoRepo)
        {
            _artesanoRepo = artesanoRepo;
        }

        public void bloquearArtesano(int id)
        {
            _artesanoRepo.bloquearArtesano(id);
        }
    }
}
