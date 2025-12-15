using ProyectoIntegrador.LogicaAplication.Dtos;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador.LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class EditarClienteCasoDeUso : IEditarCliente
    {
        private readonly IClienteRepositorio _clienteRepo;

        public EditarClienteCasoDeUso(IClienteRepositorio clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        public void Actualizar(EditarClienteDto dto)
        {
            Cliente cliente = _clienteRepo.obtenerCliente(dto.Email);

            cliente.nombre = dto.Nombre;
            cliente.apellido = dto.Apellido;
            cliente.direccion = new Direccion(
               dto.Domicilio,
               dto.Departamento,
               dto.Barrio
           );
            cliente.foto = dto.Foto;
            cliente.Validar();
            _clienteRepo.Actualizar(cliente);
        }
    }
}
