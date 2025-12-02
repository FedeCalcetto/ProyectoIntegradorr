using ProyectoIntegrador.LogicaNegocio.Entidades;
using System.ComponentModel.DataAnnotations;

namespace ProyectoIntegrador_Web.Models
{
    public class AltaProductoViewModel
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "La descripción es requerida")]
        public string descripcion { get; set; }
        [Required(ErrorMessage = "El precio es requerido")]
        public int precio { get; set; }
        [Required(ErrorMessage = "Debe subir una imagen del producto")]
        public IFormFile ArchivoImagen { get; set; }
        public string? imagen { get; set; }
        [Required(ErrorMessage = "el stock es requerido")]
        public int stock { get; set; }
        [Required(ErrorMessage = "La categoría es requerida")]
        public int? CategoriaId { get; set; }

        [Required(ErrorMessage = "La subcategoría es requerida")]
        public int? SubCategoriaId { get; set; }
        public List<IFormFile> Fotos { get; set; } = new();

        public IEnumerable<Categoria>? Categorias { get; set; }
        public List<SubCategoria> SubCategorias { get; set; } = new List<SubCategoria>();
    }
}
