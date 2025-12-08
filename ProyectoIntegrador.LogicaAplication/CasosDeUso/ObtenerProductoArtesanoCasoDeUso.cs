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
    public class ObtenerProductoArtesanoCasoDeUso : IObtenerProductoArtesano
    {

        private readonly IArtesanoRepositorio _artesanoRepo;

        public ObtenerProductoArtesanoCasoDeUso(IArtesanoRepositorio artesanoRepo)
        {
            _artesanoRepo = artesanoRepo;
        }

        public Artesano obtenerProductos(string email)
        {
            return _artesanoRepo.ObtenerProductosArtesano(email);
        }
    }
}
