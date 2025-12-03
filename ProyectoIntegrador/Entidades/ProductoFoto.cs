using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class ProductoFoto
    {
        public int Id { get; set; }
        public string UrlImagen { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}
