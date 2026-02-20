using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoIntegrador.EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class InicioFede : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carritos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carritos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoVerificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Verificado = table.Column<bool>(type: "bit", nullable: false),
                    TokenVerificacionEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenVerificacionEmailExpira = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TipoUsuario = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefono = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    Artesano_foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Artesano_bloqueado = table.Column<bool>(type: "bit", nullable: true),
                    Clienteid = table.Column<int>(type: "int", nullable: true),
                    direccion_domicilio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    direccion_departamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    direccion_barrio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bloqueado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Usuarios_Clienteid",
                        column: x => x.Clienteid,
                        principalTable: "Usuarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "SubCategorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoriaId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategorias_Categorias_categoriaId",
                        column: x => x.categoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clienteId = table.Column<int>(type: "int", nullable: true),
                    artesanoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_Comentarios_Usuarios_artesanoId",
                        column: x => x.artesanoId,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comentarios_Usuarios_clienteId",
                        column: x => x.clienteId,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ordenes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MercadoPagoPaymentId = table.Column<long>(type: "bigint", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreferenceId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ordenes_Usuarios_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PedidosPersonalizados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinalizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: true),
                    ArtesanoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosPersonalizados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidosPersonalizados_Usuarios_ArtesanoId",
                        column: x => x.ArtesanoId,
                        principalTable: "Usuarios",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_PedidosPersonalizados_Usuarios_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    precio = table.Column<int>(type: "int", nullable: false),
                    imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    ArtesanoId = table.Column<int>(type: "int", nullable: false),
                    SubCategoriaId = table.Column<int>(type: "int", nullable: false),
                    Clienteid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Productos_SubCategorias_SubCategoriaId",
                        column: x => x.SubCategoriaId,
                        principalTable: "SubCategorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Productos_Usuarios_ArtesanoId",
                        column: x => x.ArtesanoId,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Productos_Usuarios_Clienteid",
                        column: x => x.Clienteid,
                        principalTable: "Usuarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrdenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoFactura = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    ArtesanoId = table.Column<int>(type: "int", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facturas_Ordenes_OrdenId",
                        column: x => x.OrdenId,
                        principalTable: "Ordenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facturas_Usuarios_ArtesanoId",
                        column: x => x.ArtesanoId,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facturas_Usuarios_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdenItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArtesanoId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    NombreProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenItem_Ordenes_OrdenId",
                        column: x => x.OrdenId,
                        principalTable: "Ordenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenItem_Usuarios_ArtesanoId",
                        column: x => x.ArtesanoId,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarritoItems",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    carritoId = table.Column<int>(type: "int", nullable: false),
                    productoId = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoItems", x => x.id);
                    table.ForeignKey(
                        name: "FK_CarritoItems_Carritos_carritoId",
                        column: x => x.carritoId,
                        principalTable: "Carritos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarritoItems_Productos_productoId",
                        column: x => x.productoId,
                        principalTable: "Productos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductoFotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlImagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoFotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductoFotos_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reportes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    razon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clienteId = table.Column<int>(type: "int", nullable: true),
                    artesanoId = table.Column<int>(type: "int", nullable: true),
                    productoId = table.Column<int>(type: "int", nullable: true),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reportes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Reportes_Productos_productoId",
                        column: x => x.productoId,
                        principalTable: "Productos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reportes_Usuarios_artesanoId",
                        column: x => x.artesanoId,
                        principalTable: "Usuarios",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Reportes_Usuarios_clienteId",
                        column: x => x.clienteId,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LineasFactura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idProducto = table.Column<int>(type: "int", nullable: true),
                    NombreProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idFactura = table.Column<int>(type: "int", nullable: false),
                    artesanoId = table.Column<int>(type: "int", nullable: false),
                    NombreArtesano = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    precioUnitario = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineasFactura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineasFactura_Facturas_idFactura",
                        column: x => x.idFactura,
                        principalTable: "Facturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Cerámica" },
                    { 2, "Textiles" },
                    { 3, "Madera" },
                    { 4, "Cuero" },
                    { 5, "Joyería Artesanal" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "id", "CodigoVerificacion", "TipoUsuario", "TokenVerificacionEmail", "TokenVerificacionEmailExpira", "Verificado", "apellido", "nombre", "password", "rol", "email_email" },
                values: new object[] { 1, null, "ADMIN", null, null, true, "Principal", "Administrador", "Admin123456", "ADMIN", "admin@proyecto.com" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "id", "CodigoVerificacion", "TipoUsuario", "TokenVerificacionEmail", "TokenVerificacionEmailExpira", "Verificado", "apellido", "nombre", "password", "rol", "email_email", "direccion_barrio", "direccion_departamento", "direccion_domicilio" },
                values: new object[] { 2, null, "CLIENTE", null, null, true, "Cliente", "Juan", "Cliente123456", "CLIENTE", "cliente@proyecto.com", "Centro", "Montevideo", "Calle 123" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "id", "CodigoVerificacion", "TipoUsuario", "TokenVerificacionEmail", "TokenVerificacionEmailExpira", "Verificado", "apellido", "nombre", "password", "rol", "email_email" },
                values: new object[,]
                {
                    { 3, null, "ARTESANO", null, null, true, "Artesana", "Maria", "Artesano123456", "ARTESANO", "artesano@proyecto.com" },
                    { 4, null, "ARTESANO", null, null, true, "Artesana", "Ana", "Artesano123456", "ARTESANO", "artesano2@proyecto.com" }
                });

            migrationBuilder.InsertData(
                table: "SubCategorias",
                columns: new[] { "Id", "Nombre", "categoriaId" },
                values: new object[,]
                {
                    { 1, "Vasos y tazas", 1 },
                    { 2, "Platos y bowls", 1 },
                    { 3, "Esculturas cerámicas", 1 },
                    { 4, "Ropa tejida", 2 },
                    { 5, "Alfombras", 2 },
                    { 6, "Accesorios textiles", 2 },
                    { 7, "Tallados en madera", 3 },
                    { 8, "Muebles pequeños", 3 },
                    { 9, "Decoración en madera", 3 },
                    { 10, "Carteras", 4 },
                    { 11, "Cinturones", 4 },
                    { 12, "Accesorios de cuero", 4 },
                    { 13, "Collares", 5 },
                    { 14, "Pulseras", 5 },
                    { 15, "Aros", 5 }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "id", "ArtesanoId", "Clienteid", "SubCategoriaId", "descripcion", "imagen", "nombre", "precio", "stock" },
                values: new object[,]
                {
                    { 1, 3, null, 5, "Alfombra tejida a mano con lana natural", "alfombra-textil.jpg", "Alfombra Andina", 3200, 5 },
                    { 2, 3, null, 5, "Manta de algodón tejida a mano", "alfombra-textil.jpg", "Manta Textil Artesanal", 2800, 4 },
                    { 3, 3, null, 8, "Mate artesanal de madera pulida", "mate-madera.jpg", "Mate de Madera Tallado", 1200, 10 },
                    { 4, 3, null, 9, "Caja artesanal de madera natural", "mate-madera.jpg", "Caja Decorativa de Madera", 1500, 6 },
                    { 5, 4, null, 10, "Cartera hecha en cuero natural", "cartera-cuero.jpg", "Cartera de Cuero Premium", 5200, 3 },
                    { 6, 4, null, 11, "Cinturón de cuero genuino", "cartera-cuero.jpg", "Cinturón de Cuero Artesanal", 1800, 8 },
                    { 7, 4, null, 13, "Collar artesanal de plata 925", "collar-plata.jpg", "Collar de Plata", 3900, 4 },
                    { 8, 4, null, 14, "Pulsera de plata hecha a mano", "collar-plata.jpg", "Pulsera Artesanal", 2100, 7 },
                    { 9, 3, null, 1, "Taza de cerámica esmaltada", "taza-ceramica.jpg", "Taza de Cerámica", 900, 12 },
                    { 10, 3, null, 2, "Bowl artesanal de cerámica", "taza-ceramica.jpg", "Bowl de Cerámica", 1300, 6 }
                });

            migrationBuilder.InsertData(
                table: "ProductoFotos",
                columns: new[] { "Id", "ProductoId", "UrlImagen" },
                values: new object[,]
                {
                    { 1, 1, "/img/alfombra-textil.jpg" },
                    { 2, 2, "/img/alfombra-textil.jpg" },
                    { 3, 3, "/img/mate-madera.jpg" },
                    { 4, 4, "/img/mate-madera.jpg" },
                    { 5, 5, "/img/cartera-cuero.jpg" },
                    { 6, 6, "/img/cartera-cuero.jpg" },
                    { 7, 7, "/img/collar-plata.jpg" },
                    { 8, 8, "/img/collar-plata.jpg" },
                    { 9, 9, "/img/taza-ceramica.jpg" },
                    { 10, 10, "/img/taza-ceramica.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarritoItems_carritoId",
                table: "CarritoItems",
                column: "carritoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoItems_productoId",
                table: "CarritoItems",
                column: "productoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_artesanoId",
                table: "Comentarios",
                column: "artesanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_clienteId",
                table: "Comentarios",
                column: "clienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_ArtesanoId",
                table: "Facturas",
                column: "ArtesanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_ClienteId",
                table: "Facturas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_OrdenId",
                table: "Facturas",
                column: "OrdenId");

            migrationBuilder.CreateIndex(
                name: "IX_LineasFactura_idFactura",
                table: "LineasFactura",
                column: "idFactura");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_ClienteId",
                table: "Ordenes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenItem_ArtesanoId",
                table: "OrdenItem",
                column: "ArtesanoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenItem_OrdenId",
                table: "OrdenItem",
                column: "OrdenId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosPersonalizados_ArtesanoId",
                table: "PedidosPersonalizados",
                column: "ArtesanoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosPersonalizados_ClienteId",
                table: "PedidosPersonalizados",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoFotos_ProductoId",
                table: "ProductoFotos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_ArtesanoId",
                table: "Productos",
                column: "ArtesanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Clienteid",
                table: "Productos",
                column: "Clienteid");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_SubCategoriaId",
                table: "Productos",
                column: "SubCategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_artesanoId",
                table: "Reportes",
                column: "artesanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_clienteId",
                table: "Reportes",
                column: "clienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_productoId",
                table: "Reportes",
                column: "productoId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategorias_categoriaId",
                table: "SubCategorias",
                column: "categoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Clienteid",
                table: "Usuarios",
                column: "Clienteid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarritoItems");

            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "LineasFactura");

            migrationBuilder.DropTable(
                name: "OrdenItem");

            migrationBuilder.DropTable(
                name: "PedidosPersonalizados");

            migrationBuilder.DropTable(
                name: "ProductoFotos");

            migrationBuilder.DropTable(
                name: "Reportes");

            migrationBuilder.DropTable(
                name: "Carritos");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Ordenes");

            migrationBuilder.DropTable(
                name: "SubCategorias");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
