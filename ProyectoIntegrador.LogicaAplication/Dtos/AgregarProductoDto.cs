using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Dtos
{
    public class AgregarProductoDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Precio { get; set; }
        public int Stock { get; set; }
        public int SubCategoriaId { get; set; }
        public int ArtesanoId { get; set; }
        public string Imagen { get; set; }

        public List<string> Fotos { get; set; } = new();

    }
}
