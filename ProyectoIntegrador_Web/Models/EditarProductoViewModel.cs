using ProyectoIntegrador.LogicaNegocio.Entidades;
using System.ComponentModel.DataAnnotations;

namespace ProyectoIntegrador_Web.Models
{
    public class EditarProductoViewModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "La descripción es requerida")]
        public string descripcion { get; set; }
        [Required(ErrorMessage = "El precio es requerido")]
        public int precio { get; set; }
        [Required(ErrorMessage = "el stock es requerido")]
        public int stock { get; set; }
        [Required(ErrorMessage = "La categoría es requerida")]
        public int? CategoriaId { get; set; }
        [Required(ErrorMessage = "La subcategoría es requerida")]
        public int? SubCategoriaId { get; set; }
        public IEnumerable<Categoria>? Categorias { get; set; }
        public IEnumerable<SubCategoria>? SubCategorias { get; set; }
    }
}
