using Microsoft.EntityFrameworkCore;
using ProyectoIntegrador.LogicaNegocio.Entidades;

namespace ProyectoIntegrador.EntityFrameWork
{
    public class ProyectoDBContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Artesano> Artesanos { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<PedidoPersonalizado> PedidosPersonalizados { get; set; }

        public ProyectoDBContext(DbContextOptions<ProyectoDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasDiscriminator<string>("TipoUsuario")
                .HasValue<Cliente>("Cliente")
                .HasValue<Artesano>("Artesano")
                .HasValue<Admin>("Admin");
        }
    }
}
