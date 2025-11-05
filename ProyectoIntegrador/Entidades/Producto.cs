using ProyectoIntegrador.LogicaNegocio.Interface.Validacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class Producto : IValidable
    {

        public int id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "La descripción es requerida")]
        public string descripcion { get; set; }
        [Required(ErrorMessage = "El perico es requerido")]
        public int precio { get; set; }
        [Required(ErrorMessage = "la imagen es requerida")]
        public string imagen { get; set; }
        public int stock { get; set; }
        public Artesano artesano { get; set; }
        public List<Comentario> comentarios { get; set; } = new List<Comentario>();
        public int subCategroiaId { get; set; }
        public SubCategoria SubCategoria { get; set; }

        public void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
