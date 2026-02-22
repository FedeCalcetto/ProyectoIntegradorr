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
    public class ObtenerClienteConFavoritosCasoDeUso : IObtenerClienteConFavoritos
    {
        private readonly IClienteRepositorio _repositorioDeClientes;

        public ObtenerClienteConFavoritosCasoDeUso(IClienteRepositorio repo)
        {
            _repositorioDeClientes = repo;
        }

        public Cliente Ejecutar(string email)
        {
            return _repositorioDeClientes.ObtenerClienteConFavoritos(email);
        }
    }
}
