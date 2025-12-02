using Microsoft.EntityFrameworkCore;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Excepciones;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.EntityFrameWork.Repositorios
{
    public class ClienteEFRepositorio : IClienteRepositorio
    {

        private readonly ProyectoDBContext _contexto;

        public ClienteEFRepositorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }

        public void Actualizar(Cliente cliente)
        {
            cliente.validarEditar();

            var entidad = obtenerCliente(cliente.email.email);

            if(entidad is null)
            {
                throw new ClienteNoEncontradoException();
            }

            entidad.nombre = cliente.nombre;
            entidad.apellido = cliente.apellido;
            //entidad.email = cliente.email;
            //entidad.password = cliente.password;
            entidad.direccion = cliente.direccion;
            _contexto.Update(entidad);
            _contexto.SaveChanges();

        }
        public void cambioContra(Cliente cliente,string contraNueva,string contraRepetida,string contraActual)
        {
            cliente.validarContra(contraNueva,contraRepetida,contraActual);

            var entidad = obtenerCliente(cliente.email.email);

            if (entidad is null)
            {
                throw new ClienteNoEncontradoException();
            }


            entidad.password = contraNueva;
            _contexto.Update(entidad);
            _contexto.SaveChanges();
        }
        public void Agregar(Cliente entidad)
        {
            throw new NotImplementedException();
        }

        public void Editar(Cliente entidad)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            var cliente = _contexto.Usuarios
                                   .OfType<Cliente>()
                                   .FirstOrDefault(c => c.id == id);

            if (cliente == null)
                throw new ClienteNoEncontradoException();

            _contexto.Usuarios.Remove(cliente);
            _contexto.SaveChanges();
        }

        public Cliente Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public Cliente obtenerCliente(string email)
        {
            return _contexto.Usuarios
                       .OfType<Cliente>() 
                       .FirstOrDefault(c => c.email.email == email);
        }

        public IEnumerable<Cliente> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
