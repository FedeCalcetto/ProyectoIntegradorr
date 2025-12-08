using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class EliminarClienteCasoDeUso: IEliminarCliente
    {
        private readonly IObtenerCliente _obtenerCliente;
        private readonly IClienteRepositorio _clienteRepositorio;

        public EliminarClienteCasoDeUso(IObtenerCliente obtenerCliente,
                                        IClienteRepositorio clienteRepositorio)
        {
            _obtenerCliente = obtenerCliente;
            _clienteRepositorio = clienteRepositorio;
        }

        public void Ejecutar(string email)
        {
            var cliente = _obtenerCliente.Ejecutar(email);

            if (cliente == null)
                throw new Exception("El cliente no existe.");

            _clienteRepositorio.Eliminar(cliente.id);
        }
    }
}
