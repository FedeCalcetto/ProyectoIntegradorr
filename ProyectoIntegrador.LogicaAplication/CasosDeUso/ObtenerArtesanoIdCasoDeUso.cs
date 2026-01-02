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
    public class ObtenerArtesanoIdCasoDeUso : IObtenerArtesanoId
    {
        private readonly IArtesanoRepositorio _artesanoRepositorio;
        public ObtenerArtesanoIdCasoDeUso(IArtesanoRepositorio artesanoRepositorio)
        {
            _artesanoRepositorio = artesanoRepositorio;
        }

        public Artesano Ejecutar(int id)
        {
           return _artesanoRepositorio.Obtener(id);
        }
    }
}
