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
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        //public DbSet<PedidoPersonalizado> PedidosPersonalizados { get; set; }

        public ProyectoDBContext(DbContextOptions<ProyectoDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasDiscriminator<string>("TipoUusario")
                .HasValue<Artesano>("Artesano")
                .HasValue<Cliente>("Cliente")
                .HasValue<Admin>("Admin");

            modelBuilder.Entity<Usuario>().OwnsOne(u => u.email);
            modelBuilder.Entity<Cliente>().OwnsOne(c => c.direccion);

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

            modelBuilder.Entity<Usuario>().OwnsOne(u => u.email, email =>
            {
                email.Property(e => e.email).HasColumnName("email_email");

                // SEEDING del owned type Email
                email.HasData(
                    new { Usuarioid = 1, email = "juan@cliente.com" },
                    new { Usuarioid = 2, email = "laura@artesana.com" },
                    new { Usuarioid = 3, email = "admin@site.com" }
                );
            });

            modelBuilder.Entity<Cliente>().OwnsOne(c => c.direccion, direccion =>
            {
                direccion.Property(d => d.domicilio).HasColumnName("direccion_domicilio");
                direccion.Property(d => d.departamento).HasColumnName("direccion_departamento");
                direccion.Property(d => d.barrio).HasColumnName("direccion_barrio");

                // SEEDING del owned type Direccion
                direccion.HasData(
                    new
                    {
                        Clienteid = 1,
                        domicilio = "Av. Libertad 123",
                        departamento = "Montevideo",
                        barrio = "Centro"
                    }
                );
            });

            // ----- SEED ENTIDADES PRINCIPALES -----
            modelBuilder.Entity<Cliente>().HasData(new
            {
                id = 1,
                nombre = "Juan",
                apellido = "Pérez",
                password = "juancliente123",
                TipoUsuario = "Cliente"
            });

            modelBuilder.Entity<Artesano>().HasData(new
            {
                id = 2,
                nombre = "Laura",
                apellido = "Gómez",
                password = "lauraartesana123",
                foto = "laura.jpg",
                descripcion = "Artesana especializada en cerámica artesanal.",
                telefono = "099123456",
                TipoUsuario = "Artesano"
            });

            modelBuilder.Entity<Admin>().HasData(new
            {
                id = 3,
                nombre = "Admin",
                apellido = "Root",
                password = "admin123456",
                TipoUsuario = "Admin"
            });
        }
    }
    }

