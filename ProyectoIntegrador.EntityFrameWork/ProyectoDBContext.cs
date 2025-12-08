using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
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
        public DbSet<ProductoFoto> ProductoFotos { get; set; }

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

            // 🧱 Crea tabla ClienteProductoFavorito 
            //modelBuilder.Entity<Cliente>()
            //.HasMany(c => c.productosFavoritos)
            //.WithMany()
            //.UsingEntity(j => j.ToTable("ClienteProductoFavorito"));

            //// 🧱 Crea tabla ClienteArtesanoSeguido
            //modelBuilder.Entity<Cliente>()
            //.HasMany(c => c.artesanosSeguidos)
            //.WithMany()

            //.UsingEntity(j => j.ToTable("ClienteArtesanoSeguido"));

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
                .OnDelete(DeleteBehavior.Cascade);

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

            modelBuilder.Entity<Producto>()
            .HasMany(p => p.Fotos)
            .WithOne(f => f.Producto)
            .HasForeignKey(f => f.ProductoId)
            .OnDelete(DeleteBehavior.Cascade);

            // 🧪 Seed principal (TPH + owned types)
            modelBuilder.Entity<Usuario>().HasData(
                new
                {
                    id = 1,
                    nombre = "Administrador",
                    apellido = "Principal",
                    password = "Admin123456",
                    rol = "ADMIN",
                    TipoUsuario = "ADMIN",
                    CodigoVerificacion = (string?)null, //se agrega para que no de error las migraciones, el campo debe estar como nullo
                    Verificado = true    //se agrega para que no de error las migraciones, este debe ser falso siempre que se registre alguien


                },
                new
                {
                    id = 2,
                    nombre = "Juan",
                    apellido = "Cliente",
                    password = "Cliente123456",
                    rol = "CLIENTE",
                    TipoUsuario = "CLIENTE",
                    CodigoVerificacion = (string?)null,
                    Verificado = true
                },
                new
                {
                    id = 3,
                    nombre = "Maria",
                    apellido = "Artesana",
                    password = "Artesano123456",
                    rol = "ARTESANO",
                    TipoUsuario = "ARTESANO",
                    CodigoVerificacion = (string?)null,
                    Verificado = true
                }
            );

            modelBuilder.Entity<Categoria>().HasData(
            new { Id = 1, Nombre = "Cerámica" },
            new { Id = 2, Nombre = "Textiles" },
            new { Id = 3, Nombre = "Madera" },
            new { Id = 4, Nombre = "Cuero" },
            new { Id = 5, Nombre = "Joyería Artesanal" }
            );
            modelBuilder.Entity<SubCategoria>().HasData(
    // Cerámica
            new { Id = 1, Nombre = "Vasos y tazas", categoriaId = 1 },
            new { Id = 2, Nombre = "Platos y bowls", categoriaId = 1 },
            new { Id = 3, Nombre = "Esculturas cerámicas", categoriaId = 1 },

    // Textiles
            new { Id = 4, Nombre = "Ropa tejida", categoriaId = 2 },
            new { Id = 5, Nombre = "Alfombras", categoriaId = 2 },
            new { Id = 6, Nombre = "Accesorios textiles", categoriaId = 2 },

    // Madera
            new { Id = 7, Nombre = "Tallados en madera", categoriaId = 3 },
            new { Id = 8, Nombre = "Muebles pequeños", categoriaId = 3 },
            new { Id = 9, Nombre = "Decoración en madera", categoriaId = 3 },

    // Cuero
            new { Id = 10, Nombre = "Carteras", categoriaId = 4 },
            new { Id = 11, Nombre = "Cinturones", categoriaId = 4 },
            new { Id = 12, Nombre = "Accesorios de cuero", categoriaId = 4 },

    // Joyería Artesanal
            new { Id = 13, Nombre = "Collares", categoriaId = 5 },
            new { Id = 14, Nombre = "Pulseras", categoriaId = 5 },
            new { Id = 15, Nombre = "Aros", categoriaId = 5 }
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

