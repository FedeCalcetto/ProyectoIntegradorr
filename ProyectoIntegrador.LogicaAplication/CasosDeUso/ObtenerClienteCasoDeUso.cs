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
    public class ObtenerClienteCasoDeUso : IObtenerCliente
    {

        private readonly IClienteRepositorio _clienteRepositorio;

        public ObtenerClienteCasoDeUso(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public Cliente Ejecutar(string email)
        {
            return _clienteRepositorio.obtenerCliente(email);
        }
    }
}
