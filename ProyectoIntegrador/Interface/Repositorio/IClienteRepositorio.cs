using ProyectoIntegrador.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Interface.Repositorio
{
    public interface IClienteRepositorio : IRepositorio<Cliente>
    {

        Cliente obtenerCliente(string email);

        void Actualizar(Cliente cliente);
        void cambioContra(Cliente cliente,string contraNueva,string contraRepetida,string contraActual);
        void agregarArtesano(Cliente c, Artesano a);
        void eliminarArtesano(Cliente c, Artesano a);
        Cliente BuscarClientePorEmailConArtesanos(string email);
    }
}
