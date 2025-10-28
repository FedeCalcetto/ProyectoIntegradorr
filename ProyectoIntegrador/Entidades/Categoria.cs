using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class Categoria
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public List<SubCategoria> categorias { get; set; } = new List<SubCategoria>();
    }
}
