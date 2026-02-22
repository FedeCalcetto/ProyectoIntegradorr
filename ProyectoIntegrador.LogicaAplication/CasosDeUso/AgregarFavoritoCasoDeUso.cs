using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class AgregarFavoritoCasoDeUso : IAgregarFavorito
    {
        private readonly IClienteRepositorio _clienteRepo;
        private readonly IProductoRepositorio _productoRepo;

        public AgregarFavoritoCasoDeUso(IClienteRepositorio clienteRepo, IProductoRepositorio productoRepo)
        {
            _clienteRepo = clienteRepo;
            _productoRepo = productoRepo;
        }

        public void Ejecutar(string emailCliente, int productoId)
        {
            var cliente = _clienteRepo.obtenerCliente(emailCliente);
            if (cliente == null) throw new Exception("Cliente no encontrado.");

            if (cliente.productosFavoritos.Any(p => p.id == productoId))
                return; // ya estaba, no hacemos nada

            var producto = _productoRepo.Obtener(productoId);
            if (producto == null) throw new Exception("Producto no encontrado.");

            cliente.productosFavoritos.Add(producto);
            _clienteRepo.Actualizar(cliente);
        }
    }
}
