using ProyectoIntegrador.LogicaAplication.Dtos;
using ProyectoIntegrador.LogicaNegocio.Entidades;

namespace ProyectoIntegrador_Web.Models
{
    public class PerfilPublicoViewModel
    {
        public Artesano Artesano { get; set; }
        public IEnumerable<Producto> Productos { get; set; }
        public AgregarReporteDto Reporte { get; set; } = new();
    }
}
