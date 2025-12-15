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
    public class AgregarAlCarritoCasoDeUso : IAgragarAlCarrito
    {
        private readonly ICarritoRepositorio _carritoRepo;

        public AgregarAlCarritoCasoDeUso(ICarritoRepositorio carritoRepo)
        {
            _carritoRepo = carritoRepo;
        }
        public void agregarAlCarrito(int idProducto, int idUsuario, int cantidad)
        {
            var carritoItem = _carritoRepo.BuscarProducto(idProducto, idUsuario);

            if (carritoItem == null)
            {
                // si no existe, lo agregamos
                _carritoRepo.AgregarItem(idUsuario, idProducto, cantidad);
            }
            else
            {
                // si existe, sumamos cantidad
                carritoItem.cantidad += cantidad;
                _carritoRepo.Guardar();
            }
        }

    }
    }

