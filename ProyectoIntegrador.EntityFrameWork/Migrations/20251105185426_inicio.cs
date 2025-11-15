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
                    TipoUsuario = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefono = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    Clienteid = table.Column<int>(type: "int", nullable: true),
                    direccion_domicilio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    direccion_departamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    direccion_barrio = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "SubCategoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoriaId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategoria_Categorias_categoriaId",
                        column: x => x.categoriaId,
                        principalTable: "Categorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoPersonalizado",
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
                    table.PrimaryKey("PK_PedidoPersonalizado", x => x.id);
                    table.ForeignKey(
                        name: "FK_PedidoPersonalizado_Usuarios_artesanoId",
                        column: x => x.artesanoId,
                        principalTable: "Usuarios",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_PedidoPersonalizado_Usuarios_clienteId",
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
                    Clienteid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Productos_SubCategoria_subCategroiaId",
                        column: x => x.subCategroiaId,
                        principalTable: "SubCategoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Comentarios_Usuarios_artesanoId",
                        column: x => x.artesanoId,
                        principalTable: "Usuarios",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Comentarios_Usuarios_clienteId",
                        column: x => x.clienteId,
                        principalTable: "Usuarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "LineaFactura",
                columns: table => new
                {
                    idProducto = table.Column<int>(type: "int", nullable: false),
                    idFactura = table.Column<int>(type: "int", nullable: false),
                    precioUnitario = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineaFactura", x => new { x.idProducto, x.idFactura });
                    table.ForeignKey(
                        name: "FK_LineaFactura_Facturas_idFactura",
                        column: x => x.idFactura,
                        principalTable: "Facturas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LineaFactura_Productos_idProducto",
                        column: x => x.idProducto,
                        principalTable: "Productos",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "id", "TipoUsuario", "apellido", "nombre", "password", "rol", "email_email", "direccion_barrio", "direccion_departamento", "direccion_domicilio" },
                values: new object[] { 1, "Cliente", "Pérez", "Juan", "juancliente123", "CLIENTE", "juan@cliente.com", "Centro", "Montevideo", "Av. Libertad 123" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "id", "TipoUsuario", "apellido", "nombre", "password", "rol", "email_email" },
                values: new object[,]
                {
                    { 2, "Artesano", "Gómez", "Laura", "lauraartesana123", "ARTESANO", "laura@artesana.com" },
                    { 3, "Admin", "Root", "Admin", "admin123456", "ADMIN", "admin@site.com" }
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
                name: "IX_LineaFactura_idFactura",
                table: "LineaFactura",
                column: "idFactura");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoPersonalizado_artesanoId",
                table: "PedidoPersonalizado",
                column: "artesanoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoPersonalizado_clienteId",
                table: "PedidoPersonalizado",
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
                name: "IX_Productos_subCategroiaId",
                table: "Productos",
                column: "subCategroiaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoria_categoriaId",
                table: "SubCategoria",
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
                name: "PedidoPersonalizado");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "SubCategoria");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
