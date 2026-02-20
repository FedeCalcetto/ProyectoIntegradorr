using ProyectoIntegrador.LogicaNegocio.Excepciones;
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
        [Required(ErrorMessage = "El precio es requerido")]
        public int precio { get; set; }
        [Required(ErrorMessage = "la imagen es requerida")]
        public string imagen { get; set; }
        public List<ProductoFoto> Fotos { get; set; } = new();
        [Required(ErrorMessage = "el stock es requerido")]
        public int stock { get; set; }
        public int ArtesanoId { get; set; }
        public Artesano artesano { get; set; }
        public int SubCategoriaId { get; set; }
        public SubCategoria SubCategoria { get; set; }


        public List<Calificación> Calificaciones { get; set; } = new();


        public void Validar()
        {
            validarNegativo();
        }

        public void validarNegativo()
        {
            if (stock <1 || precio < 1)
            {
                throw new PrecioStockException();
            }
        }

        public void DescontarStock(int cantidad)
        {
            if (stock < cantidad)
                throw new Exception("Stock insuficiente");

            stock -= cantidad;
        }
    }
}
