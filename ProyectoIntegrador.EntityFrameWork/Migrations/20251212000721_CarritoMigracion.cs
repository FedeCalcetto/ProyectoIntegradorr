using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoIntegrador.EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class CarritoMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineaFactura_Facturas_facturaId",
                table: "LineaFactura");

            migrationBuilder.DropForeignKey(
                name: "FK_LineaFactura_Productos_productoid",
                table: "LineaFactura");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LineaFactura",
                table: "LineaFactura");

            migrationBuilder.RenameTable(
                name: "LineaFactura",
                newName: "LineasFactura");

            migrationBuilder.RenameIndex(
                name: "IX_LineaFactura_productoid",
                table: "LineasFactura",
                newName: "IX_LineasFactura_productoid");

            migrationBuilder.RenameIndex(
                name: "IX_LineaFactura_facturaId",
                table: "LineasFactura",
                newName: "IX_LineasFactura_facturaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LineasFactura",
                table: "LineasFactura",
                columns: new[] { "idProducto", "idFactura" });

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

            migrationBuilder.CreateIndex(
                name: "IX_CarritoItems_carritoId",
                table: "CarritoItems",
                column: "carritoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoItems_productoId",
                table: "CarritoItems",
                column: "productoId");

            migrationBuilder.AddForeignKey(
                name: "FK_LineasFactura_Facturas_facturaId",
                table: "LineasFactura",
                column: "facturaId",
                principalTable: "Facturas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LineasFactura_Productos_productoid",
                table: "LineasFactura",
                column: "productoid",
                principalTable: "Productos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineasFactura_Facturas_facturaId",
                table: "LineasFactura");

            migrationBuilder.DropForeignKey(
                name: "FK_LineasFactura_Productos_productoid",
                table: "LineasFactura");

            migrationBuilder.DropTable(
                name: "CarritoItems");

            migrationBuilder.DropTable(
                name: "Carritos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LineasFactura",
                table: "LineasFactura");

            migrationBuilder.RenameTable(
                name: "LineasFactura",
                newName: "LineaFactura");

            migrationBuilder.RenameIndex(
                name: "IX_LineasFactura_productoid",
                table: "LineaFactura",
                newName: "IX_LineaFactura_productoid");

            migrationBuilder.RenameIndex(
                name: "IX_LineasFactura_facturaId",
                table: "LineaFactura",
                newName: "IX_LineaFactura_facturaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LineaFactura",
                table: "LineaFactura",
                columns: new[] { "idProducto", "idFactura" });

            migrationBuilder.AddForeignKey(
                name: "FK_LineaFactura_Facturas_facturaId",
                table: "LineaFactura",
                column: "facturaId",
                principalTable: "Facturas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LineaFactura_Productos_productoid",
                table: "LineaFactura",
                column: "productoid",
                principalTable: "Productos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
