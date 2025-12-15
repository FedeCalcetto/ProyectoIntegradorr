using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class Carrito
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public List<CarritoItem> Items { get; set; }
    }
}
