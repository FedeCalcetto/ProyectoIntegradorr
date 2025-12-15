using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Dtos
{
    public class CategoriaCatalogoDto
    {
        public string Nombre { get; set; }
        public List<ProductoCatalogoDto> Productos { get; set; } = new();
    }
}
