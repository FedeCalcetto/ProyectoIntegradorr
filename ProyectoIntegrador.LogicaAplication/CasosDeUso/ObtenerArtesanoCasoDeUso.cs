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
    public class ObtenerArtesanoCasoDeUso : IObtenerArtesano
    {
        private readonly IArtesanoRepositorio _artesanoRepositorio;

        public ObtenerArtesanoCasoDeUso(IArtesanoRepositorio artesanoRepositorio)
        {
            _artesanoRepositorio = artesanoRepositorio;
        }

        public Artesano Ejecutar(string email)
        {
            return _artesanoRepositorio.ObtenerPorEmail(email);
        }
    }
}
