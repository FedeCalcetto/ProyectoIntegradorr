using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Dtos
{
    public class CatalogoDto
    {
        public List<ProductoCatalogoDto> Recientes { get; set; } = new();
        public List<CategoriaCatalogoDto> PorCategoria { get; set; } = new();
    }
}
