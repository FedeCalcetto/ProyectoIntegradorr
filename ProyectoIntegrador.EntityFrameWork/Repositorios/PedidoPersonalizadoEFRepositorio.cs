using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;

namespace ProyectoIntegrador.EntityFrameWork.Repositorios
{
    public class PedidoPersonalizadoEFRepositorio : IPedidoPersonalizadoRepsoitorio
    {
        private readonly ProyectoDBContext _contexto;

        public PedidoPersonalizadoEFRepositorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }

        // ➕ Crear pedido
        public void Agregar(PedidoPersonalizado entidad)
        {
            _contexto.PedidosPersonalizados.Add(entidad);
            _contexto.SaveChanges();
        }

        // ✏️ Editar (aceptar, finalizar, etc)
        public void Editar(PedidoPersonalizado entidad)
        {
            _contexto.PedidosPersonalizados.Update(entidad);
            _contexto.SaveChanges();
        }

        // 🗑️ Eliminar
        public void Eliminar(int id)
        {
            var pedido = _contexto.PedidosPersonalizados.Find(id);
            if (pedido != null)
            {
                _contexto.PedidosPersonalizados.Remove(pedido);
                _contexto.SaveChanges();
            }
        }

        // 🔍 Obtener por ID
        public PedidoPersonalizado Obtener(int id)
        {
            return _contexto.PedidosPersonalizados
                .Include(p => p.Cliente)
                .Include(p => p.Artesano)
                .FirstOrDefault(p => p.Id == id);
        }

        // 📦 Todos
        public IEnumerable<PedidoPersonalizado> ObtenerTodos()
        {
            return _contexto.PedidosPersonalizados
                .Include(p => p.Cliente)
                .Include(p => p.Artesano)
                .ToList();
        }

        // 🟡 Pedidos que aún nadie tomó
        public List<PedidoPersonalizado> ObtenerPendientes()
        {
            return _contexto.PedidosPersonalizados
                .Include(p => p.Cliente)
                .Where(p => p.Estado == EstadoPedido.Pendiente)
                .OrderByDescending(p => p.FechaCreacion)
                .ToList();
        }

        // 🧑‍🎨 Pedidos de un artesano
        public List<PedidoPersonalizado> ObtenerPorArtesano(string emailArtesano)
        {
            return _contexto.PedidosPersonalizados
                .Include(p => p.Cliente)
                .Include(p => p.Artesano)
                .Where(p => p.Artesano.email.email == emailArtesano)
                .OrderByDescending(p => p.FechaCreacion)
                .ToList();
        }
    }
}
