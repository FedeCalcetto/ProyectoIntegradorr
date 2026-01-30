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
        public DbSet<FacturaNoFiscal> Facturas { get; set; }
        public DbSet<LineaFactura> LineasFactura { get; set; }
        public DbSet<PedidoPersonalizado> PedidosPersonalizados { get; set; }
        public DbSet<Reporte> Reportes { get; set; }
        public DbSet<ProductoFoto> ProductoFotos { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<CarritoItem> CarritoItems { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
         


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

            modelBuilder.Entity<Usuario>()
                 .Property(u => u.password)
                 .HasMaxLength(255)
                 .IsRequired();

            
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

            modelBuilder.Entity<LineaFactura>()
            .HasKey(lf => lf.Id);
            modelBuilder.Entity<LineaFactura>()
                .HasOne<FacturaNoFiscal>()
                .WithMany(f => f.itemsFactura)
                .HasForeignKey(lf => lf.idFactura)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<FacturaNoFiscal>()
            .ToTable("Facturas")
            .HasDiscriminator<string>("TipoFactura")
            .HasValue<FacturaNoFiscalCliente>("CLIENTE")
            .HasValue<FacturaNoFiscalArtesano>("ARTESANO");

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
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reporte>()
                .HasOne(r => r.producto)
                .WithMany()
                .HasForeignKey(r => r.productoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Producto>()
            .HasMany(p => p.Fotos)
            .WithOne(f => f.Producto)
            .HasForeignKey(f => f.ProductoId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductoFoto>()
            .Property(p => p.Id)
             .HasColumnName("Id");

            modelBuilder.Entity<PedidoPersonalizado>()
                .HasOne(p => p.Cliente)
                .WithMany()
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PedidoPersonalizado>()
                .HasOne(p => p.Artesano)
                .WithMany()
                .HasForeignKey(p => p.ArtesanoId)
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
         TipoUsuario = "ADMIN",
         CodigoVerificacion = (string?)null,
         Verificado = true,
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
         Verificado = true,
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
         Verificado = true,
     },
     new
     {
         id = 4,
         nombre = "Ana",
         apellido = "Artesana",
         password = "Artesano123456",
         rol = "ARTESANO",
         TipoUsuario = "ARTESANO",
         CodigoVerificacion = (string?)null,
         Verificado = true,
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


            modelBuilder.Entity<Producto>().HasData(

          new { id = 1, nombre = "Alfombra Andina", precio = 3200, stock = 5, descripcion = "Alfombra tejida a mano con lana natural", SubCategoriaId = 5, ArtesanoId = 3, imagen = "alfombra-textil.jpg" },
          new { id = 2, nombre = "Manta Textil Artesanal", precio = 2800, stock = 4, descripcion = "Manta de algodón tejida a mano", SubCategoriaId = 5, ArtesanoId = 3, imagen = "alfombra-textil.jpg" },

          new { id = 3, nombre = "Mate de Madera Tallado", precio = 1200, stock = 10, descripcion = "Mate artesanal de madera pulida", SubCategoriaId = 8, ArtesanoId = 3, imagen = "mate-madera.jpg" },
          new { id = 4, nombre = "Caja Decorativa de Madera", precio = 1500, stock = 6, descripcion = "Caja artesanal de madera natural", SubCategoriaId = 9, ArtesanoId = 3, imagen = "mate-madera.jpg" },

          new { id = 5, nombre = "Cartera de Cuero Premium", precio = 5200, stock = 3, descripcion = "Cartera hecha en cuero natural", SubCategoriaId = 10, ArtesanoId = 4, imagen = "cartera-cuero.jpg" },
          new { id = 6, nombre = "Cinturón de Cuero Artesanal", precio = 1800, stock = 8, descripcion = "Cinturón de cuero genuino", SubCategoriaId = 11, ArtesanoId = 4, imagen = "cartera-cuero.jpg" },

          new { id = 7, nombre = "Collar de Plata", precio = 3900, stock = 4, descripcion = "Collar artesanal de plata 925", SubCategoriaId = 13, ArtesanoId = 4, imagen = "collar-plata.jpg" },
          new { id = 8, nombre = "Pulsera Artesanal", precio = 2100, stock = 7, descripcion = "Pulsera de plata hecha a mano", SubCategoriaId = 14, ArtesanoId = 4, imagen = "collar-plata.jpg" },

          new { id = 9, nombre = "Taza de Cerámica", precio = 900, stock = 12, descripcion = "Taza de cerámica esmaltada", SubCategoriaId = 1, ArtesanoId = 3, imagen = "taza-ceramica.jpg" },
          new { id = 10, nombre = "Bowl de Cerámica", precio = 1300, stock = 6, descripcion = "Bowl artesanal de cerámica", SubCategoriaId = 2, ArtesanoId = 3, imagen = "taza-ceramica.jpg" }
      );



            modelBuilder.Entity<ProductoFoto>().HasData(
    new { Id = 1, ProductoId = 1, UrlImagen = "/img/alfombra-textil.jpg" },
    new { Id = 2, ProductoId = 2, UrlImagen = "/img/alfombra-textil.jpg" },
    new { Id = 3, ProductoId = 3, UrlImagen = "/img/mate-madera.jpg" },
    new { Id = 4, ProductoId = 4, UrlImagen = "/img/mate-madera.jpg" },
    new { Id = 5, ProductoId = 5, UrlImagen = "/img/cartera-cuero.jpg" },
    new { Id = 6, ProductoId = 6, UrlImagen = "/img/cartera-cuero.jpg" },
    new { Id = 7, ProductoId = 7, UrlImagen = "/img/collar-plata.jpg" },
    new { Id = 8, ProductoId = 8, UrlImagen = "/img/collar-plata.jpg" },
    new { Id = 9, ProductoId = 9, UrlImagen = "/img/taza-ceramica.jpg" },
    new { Id = 10, ProductoId = 10, UrlImagen = "/img/taza-ceramica.jpg" }
);






            // ✅ Seeding de propiedades owned (desde EF Core 8+)
            modelBuilder.Entity<Usuario>().OwnsOne(u => u.email).HasData(
                new { Usuarioid = 1, email = "admin@proyecto.com" },
                new { Usuarioid = 2, email = "cliente@proyecto.com" },
                new { Usuarioid = 3, email = "artesano@proyecto.com" },
                new { Usuarioid = 4, email = "artesano2@proyecto.com" }
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
            //Orden
            modelBuilder.Entity<Orden>()
              .HasOne(o => o.Cliente)
              .WithMany()
              .HasForeignKey(o => o.ClienteId)
              .OnDelete(DeleteBehavior.Restrict);

        }




    }
}

