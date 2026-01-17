using ProyectoIntegrador.LogicaNegocio.Entidades;

namespace ProyectoIntegrador_Web.Models
{
    public class BusquedaDeUsuariosViewModel
    {

        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public string Filtro { get; set; }
        public Usuario usuarioLogueado { get; set; }
        public bool EstaLogueado => usuarioLogueado != null;
        public bool UsuarioLogueadoEsCliente => usuarioLogueado is Cliente;
        public bool UsuarioLogueadoEsArtesano => usuarioLogueado is Artesano;
    }
}
