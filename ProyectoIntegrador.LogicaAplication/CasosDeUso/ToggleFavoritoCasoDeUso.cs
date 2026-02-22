using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class ToggleFavoritoCasoDeUso : IToggleFavorito
    {
        private readonly IClienteRepositorio _clienteRepo;
        private readonly IObtenerProducto _obtenerProducto;

        public ToggleFavoritoCasoDeUso(IClienteRepositorio clienteRepo,
                              IObtenerProducto obtenerProducto)
        {
            _clienteRepo = clienteRepo;
            _obtenerProducto = obtenerProducto;
        }

        public bool Ejecutar(string emailCliente, int productoId)
        {
            var cliente = _clienteRepo.ObtenerClienteConFavoritos(emailCliente);
            var producto = _obtenerProducto.obtener(productoId);

            if (cliente == null || producto == null)
                throw new Exception("Cliente o producto no encontrado");

            var yaEsFav = cliente.productosFavoritos
                                 .Any(p => p.id == productoId);

            if (yaEsFav)
            {
                var prod = cliente.productosFavoritos
                                  .First(p => p.id == productoId);
                cliente.productosFavoritos.Remove(prod);
            }
            else
            {
                cliente.productosFavoritos.Add(producto);
            }

            _clienteRepo.Actualizar(cliente);

            return !yaEsFav; // devuelve nuevo estado
        }
    }
}