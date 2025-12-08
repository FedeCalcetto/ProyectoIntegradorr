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
    public class EditarArtesanoCasoDeUso : IEditarArtesano
    {
        private readonly IArtesanoRepositorio _artesanoRepo;

        public EditarArtesanoCasoDeUso(IArtesanoRepositorio artesanoRepo)
        {
            _artesanoRepo = artesanoRepo;
        }
        public void Actualizar(Artesano artesano)
        {
            _artesanoRepo.Actualizar(artesano);
        }
    }
}
