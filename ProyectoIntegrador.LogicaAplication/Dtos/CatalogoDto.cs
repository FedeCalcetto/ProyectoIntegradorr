using ProyectoIntegrador.LogicaNegocio.Entidades;
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


        /*public int? CategoriaId { get; set; }
        public int? SubCategoriaId { get; set; }
        public IEnumerable<Categoria>? Categorias { get; set; } = new List<Categoria>();
        public IEnumerable<SubCategoria>? SubCategorias { get; set; } = new List<SubCategoria>();*/
    }
}
