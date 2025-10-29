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
        //public DbSet<PedidoPersonalizado> PedidosPersonalizados { get; set; }

        public ProyectoDBContext(DbContextOptions<ProyectoDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().ToTable("Usuarios"); // Tabla base
            modelBuilder.Entity<Cliente>().ToTable("Clientes"); // Subclase
            modelBuilder.Entity<Artesano>().ToTable("Artesanos");
            modelBuilder.Entity<Admin>().ToTable("Admins");


            modelBuilder.Entity<PedidoPersonalizado>()
                .HasOne(p => p.cliente)
                .WithMany() // sin colección inversa
                .HasForeignKey(p => p.clienteId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // PedidoPersonalizado → Artesano (nullable + FK Restrict)
            modelBuilder.Entity<PedidoPersonalizado>()
                .HasOne(p => p.artesano)
                .WithMany()
                .HasForeignKey(p => p.artesanoId)
                .OnDelete(DeleteBehavior.ClientSetNull);


            // Comentario → Cliente / Artesano

            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.producto)
                .WithMany(cl => cl.comentarios)
                .HasForeignKey(c => c.productoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<LineaFactura>()
                .HasKey(lf => new { lf.idProducto, lf.idFactura });

            modelBuilder.Entity<LineaFactura>()
                .HasOne(lf => lf.producto)
                .WithMany() // o WithMany() si no hay colección
                .HasForeignKey(lf => lf.idProducto)
                .OnDelete(DeleteBehavior.ClientSetNull); // o ClientSetNull si quieres

            modelBuilder.Entity<LineaFactura>()
                .HasOne(lf => lf.factura)
                .WithMany(f => f.itemsFactura) // o WithMany() si no hay colección
                .HasForeignKey(lf => lf.idFactura)
                .OnDelete(DeleteBehavior.ClientSetNull); // evita cascadas múltiples

        }
    }
}
