using ProyectoIntegrador.LogicaNegocio.Entidades;

namespace ProyectoIntegrador_Web.Models
{
    public class BusquedaDeUsuariosViewModel
    {

        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public string Filtro { get; set; }

    }
}
