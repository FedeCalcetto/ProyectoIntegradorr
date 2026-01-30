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
    public class ObtenerArtesanoDashboardCasoDeUso :IObtenerArtesanoDashboard
    {
        private readonly IArtesanoRepositorio _artesanoRepo;

        public ObtenerArtesanoDashboardCasoDeUso(IArtesanoRepositorio artesanoRepo)
        {
            _artesanoRepo = artesanoRepo;
        }

        public Artesano ObtenerArtesanoDashboard(string email)
        {
            return _artesanoRepo.ObtenerArtesanoDashboard( email);
        }
    }
}
