using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoIntegrador.EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.id);
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
                    password = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    direccion_domicilio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    direccion_departamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    direccion_barrio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoUsuario = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefono = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    Clienteid = table.Column<int>(type: "int", nullable: true)
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
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Clienteid = table.Column<int>(type: "int", nullable: false),
                    Artesanoid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facturas_Usuarios_Artesanoid",
                        column: x => x.Artesanoid,
                        principalTable: "Usuarios",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Facturas_Usuarios_Clienteid",
                        column: x => x.Clienteid,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PedidosPersonalizados",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clienteId = table.Column<int>(type: "int", nullable: true),
                    artesanoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosPersonalizados", x => x.id);
                    table.ForeignKey(
                        name: "FK_PedidosPersonalizados_Usuarios_artesanoId",
                        column: x => x.artesanoId,
                        principalTable: "Usuarios",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_PedidosPersonalizados_Usuarios_clienteId",
                        column: x => x.clienteId,
                        principalTable: "Usuarios",
                        principalColumn: "id");
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
                    artesanoid = table.Column<int>(type: "int", nullable: false),
                    subCategroiaId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Productos_Usuarios_Clienteid",
                        column: x => x.Clienteid,
                        principalTable: "Usuarios",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Productos_Usuarios_artesanoid",
                        column: x => x.artesanoid,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clienteId = table.Column<int>(type: "int", nullable: true),
                    artesanoId = table.Column<int>(type: "int", nullable: true),
                    productoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_Comentarios_Productos_productoId",
                        column: x => x.productoId,
                        principalTable: "Productos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LineaFactura",
                columns: table => new
                {
                    idProducto = table.Column<int>(type: "int", nullable: false),
                    idFactura = table.Column<int>(type: "int", nullable: false),
                    productoid = table.Column<int>(type: "int", nullable: false),
                    facturaId = table.Column<int>(type: "int", nullable: false),
                    precioUnitario = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineaFactura", x => new { x.idProducto, x.idFactura });
                    table.ForeignKey(
                        name: "FK_LineaFactura_Facturas_facturaId",
                        column: x => x.facturaId,
                        principalTable: "Facturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineaFactura_Productos_productoid",
                        column: x => x.productoid,
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
                    productoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reportes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Reportes_Productos_productoId",
                        column: x => x.productoId,
                        principalTable: "Productos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reportes_Usuarios_artesanoId",
                        column: x => x.artesanoId,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reportes_Usuarios_clienteId",
                        column: x => x.clienteId,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "id", "TipoUsuario", "apellido", "foto", "nombre", "password", "rol", "direccion_barrio", "direccion_departamento", "direccion_domicilio", "email_email" },
                values: new object[,]
                {
                    { 1, "ADMIN", "Principal", null, "Administrador", "Admin123456", "ADMIN", "Centro", "Montevideo", "Calle 123", "admin@proyecto.com" },
                    { 2, "CLIENTE", "Cliente", null, "Juan", "Cliente123456", "CLIENTE", "Centro2", "Montevideo", "Calle 1234", "cliente@proyecto.com" },
                    { 3, "ARTESANO", "Artesana", null, "Maria", "Artesano123456", "ARTESANO", "Centro3", "Montevideo", "Calle 12345", "artesano@proyecto.com" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_artesanoId",
                table: "Comentarios",
                column: "artesanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_clienteId",
                table: "Comentarios",
                column: "clienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_productoId",
                table: "Comentarios",
                column: "productoId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_Artesanoid",
                table: "Facturas",
                column: "Artesanoid");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_Clienteid",
                table: "Facturas",
                column: "Clienteid");

            migrationBuilder.CreateIndex(
                name: "IX_LineaFactura_facturaId",
                table: "LineaFactura",
                column: "facturaId");

            migrationBuilder.CreateIndex(
                name: "IX_LineaFactura_productoid",
                table: "LineaFactura",
                column: "productoid");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosPersonalizados_artesanoId",
                table: "PedidosPersonalizados",
                column: "artesanoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosPersonalizados_clienteId",
                table: "PedidosPersonalizados",
                column: "clienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_artesanoid",
                table: "Productos",
                column: "artesanoid");

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
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "LineaFactura");

            migrationBuilder.DropTable(
                name: "PedidosPersonalizados");

            migrationBuilder.DropTable(
                name: "Reportes");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "SubCategorias");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
