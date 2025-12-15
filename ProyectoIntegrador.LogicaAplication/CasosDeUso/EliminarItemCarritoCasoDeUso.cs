using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador.LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class EliminarItemCarritoCasoDeUso : IEliminarItemDelCarrito
    {
        private readonly ICarritoRepositorio _carritoRepo;

        public EliminarItemCarritoCasoDeUso(ICarritoRepositorio carritoRepo)
        {
            _carritoRepo = carritoRepo;
        }
        public void Eliminar(int carritoItemId, int cantidadABorrar)
        {
            _carritoRepo.EliminarItem(carritoItemId, cantidadABorrar);
        }
    }
}
