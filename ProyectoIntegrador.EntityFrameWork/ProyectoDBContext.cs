using Microsoft.EntityFrameworkCore;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.ValueObjects;

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
        public DbSet<SubCategoria> SubCategorias { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<PedidoPersonalizado> PedidosPersonalizados { get; set; }
        public DbSet<Reporte> Reportes { get; set; }


        public ProyectoDBContext(DbContextOptions<ProyectoDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🧱 TPH: Table Per Hierarchy para Usuario
            modelBuilder.Entity<Usuario>()
                .ToTable("Usuarios")
                .HasDiscriminator<string>("TipoUsuario")
                .HasValue<Cliente>("CLIENTE")
                .HasValue<Artesano>("ARTESANO")
                .HasValue<Admin>("ADMIN");

            // 🔹 Propiedades "owned" (Email, Direccion)
            modelBuilder.Entity<Usuario>().OwnsOne(u => u.email, email =>
            {
                email.Property(e => e.email).HasColumnName("email_email");

            });

            modelBuilder.Entity<Cliente>().OwnsOne(c => c.direccion, dir =>
            {
                dir.Property(d => d.domicilio).HasColumnName("direccion_domicilio");
                dir.Property(d => d.departamento).HasColumnName("direccion_departamento");
                dir.Property(d => d.barrio).HasColumnName("direccion_barrio");
            });

            // 🔹 Relaciones
            modelBuilder.Entity<Artesano>()
                .HasMany(a => a.productos)
                .WithOne(p => p.artesano)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.compras)
                .WithOne(f => f.Cliente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Factura>()
                .HasMany(f => f.itemsFactura)
                .WithOne(lf => lf.factura)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LineaFactura>()
                .HasKey(lf => new { lf.idProducto, lf.idFactura });

            // 🧩 Comentarios
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.cliente)
                .WithMany()
                .HasForeignKey(c => c.clienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.artesano)
                .WithMany()
                .HasForeignKey(c => c.artesanoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.producto)
                .WithMany(p => p.comentarios)
                .HasForeignKey(c => c.productoId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🧩 Reportes
            modelBuilder.Entity<Reporte>()
                .HasOne(r => r.cliente)
                .WithMany()
                .HasForeignKey(r => r.clienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reporte>()
                .HasOne(r => r.artesano)
                .WithMany()
                .HasForeignKey(r => r.artesanoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reporte>()
                .HasOne(r => r.producto)
                .WithMany()
                .HasForeignKey(r => r.productoId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🧪 Seed principal (TPH + owned types)
            modelBuilder.Entity<Usuario>().HasData(
                new
                {
                    id = 1,
                    nombre = "Administrador",
                    apellido = "Principal",
                    password = "Admin123456",
                    rol = "ADMIN",
                    TipoUsuario = "ADMIN"
                },
                new
                {
                    id = 2,
                    nombre = "Juan",
                    apellido = "Cliente",
                    password = "Cliente123456",
                    rol = "CLIENTE",
                    TipoUsuario = "CLIENTE"
                },
                new
                {
                    id = 3,
                    nombre = "Maria",
                    apellido = "Artesana",
                    password = "Artesano123456",
                    rol = "ARTESANO",
                    TipoUsuario = "ARTESANO"
                }
            );

            // ✅ Seeding de propiedades owned (desde EF Core 8+)
            modelBuilder.Entity<Usuario>().OwnsOne(u => u.email).HasData(
                new { Usuarioid = 1, email = "admin@proyecto.com" },
                new { Usuarioid = 2, email = "cliente@proyecto.com" },
                new { Usuarioid = 3, email = "artesano@proyecto.com" }
            );

            modelBuilder.Entity<Cliente>().OwnsOne(c => c.direccion).HasData(
     new
     {
         Clienteid = 2,
         domicilio = "Calle 123",
         departamento = "Montevideo",
         barrio = "Centro"
     }
     
 );
        }

    }
}

