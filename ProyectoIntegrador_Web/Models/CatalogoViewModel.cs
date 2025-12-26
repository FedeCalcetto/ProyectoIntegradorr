using ProyectoIntegrador.LogicaAplication.Dtos;

namespace ProyectoIntegrador_Web.Models
{
    public class CatalogoViewModel
    {
        public CatalogoDto Catalogo { get; set; }

        // Buscador reutilizable (capa web)
        public ProductosFiltradosViewModel Buscador { get; set; }
    }
}
