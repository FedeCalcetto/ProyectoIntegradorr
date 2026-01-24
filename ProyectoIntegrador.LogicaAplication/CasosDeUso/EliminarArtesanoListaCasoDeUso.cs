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
    public class EliminarArtesanoListaCasoDeUso :IEliminarArtesanoLista
    {
        private readonly IClienteRepositorio _clienteRepo;

        public EliminarArtesanoListaCasoDeUso(IClienteRepositorio clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        public void eliminarArtesano(Cliente c, Artesano a)
        {
            _clienteRepo.eliminarArtesano(c,a);  
        }
    }
}
