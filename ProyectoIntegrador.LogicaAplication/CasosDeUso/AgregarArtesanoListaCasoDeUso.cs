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
    public class AgregarArtesanoListaCasoDeUso : IAgregarArtesanoLista
    {
        private readonly IClienteRepositorio _clienteRepo;

        public AgregarArtesanoListaCasoDeUso(IClienteRepositorio clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        public void agregarArtesano(Cliente c, Artesano a)
        {
            _clienteRepo.agregarArtesano(c, a);
        }
    }
}
