using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Dtos
{
    public class ProductoCatalogoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public string Imagen { get; set; }
        public string Artesano { get; set; }

        public string Categoria { get; set; }
        public string SubCategoria { get; set; }
        public bool EsFavorito { get; set; }

    }
}
