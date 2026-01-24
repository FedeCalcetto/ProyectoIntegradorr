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
    public class ObtenerPedidosPendientesCasoDeUso : IObtenerPedidosPendientes
    {
        private readonly IPedidoPersonalizadoRepsoitorio _repo;

        public ObtenerPedidosPendientesCasoDeUso(IPedidoPersonalizadoRepsoitorio repo)
        {
            _repo = repo;
        }

        //public IEnumerable<PedidoPersonalizado> Ejecutar()
        //{
        //    return _repo.ObtenerPendientes();
        //}

        public IEnumerable<PedidoPersonalizado> Ejecutar(string busqueda, string orden)
        {
            var pedidos = _repo.ObtenerPendientes();

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                pedidos = pedidos.Where(p =>
                    p.Titulo.Contains(busqueda, StringComparison.OrdinalIgnoreCase) || //StringComparison.OrdinalIgnoreCase 
                    p.Cliente.nombre.Contains(busqueda, StringComparison.OrdinalIgnoreCase) || //no compara mayusculas ni minusculas
                    p.Cliente.apellido.Contains(busqueda, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            pedidos = orden switch
            {
                "fecha_asc" => pedidos.OrderBy(p => p.FechaCreacion).ToList(),
                "nombre_asc" => pedidos.OrderBy(p => p.Cliente.nombre).ToList(),
                "nombre_desc" => pedidos.OrderByDescending(p => p.Cliente.nombre).ToList(),
                _ => pedidos.OrderByDescending(p => p.FechaCreacion).ToList()
            };

            return pedidos;
        }
    }

}
