using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoIntegrador.EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class FixFavoritosCascade22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Usuarios_Clienteid",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_Clienteid",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Clienteid",
                table: "Productos");

            migrationBuilder.CreateTable(
                name: "ClienteProductoFavorito",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteProductoFavorito", x => new { x.ClienteId, x.ProductoId });
                    table.ForeignKey(
                        name: "FK_ClienteProductoFavorito_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ClienteProductoFavorito_Usuarios_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Usuarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClienteProductoFavorito_ProductoId",
                table: "ClienteProductoFavorito",
                column: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteProductoFavorito");

            migrationBuilder.AddColumn<int>(
                name: "Clienteid",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 1,
                column: "Clienteid",
                value: null);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 2,
                column: "Clienteid",
                value: null);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 3,
                column: "Clienteid",
                value: null);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 4,
                column: "Clienteid",
                value: null);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 5,
                column: "Clienteid",
                value: null);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 6,
                column: "Clienteid",
                value: null);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 7,
                column: "Clienteid",
                value: null);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 8,
                column: "Clienteid",
                value: null);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 9,
                column: "Clienteid",
                value: null);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 10,
                column: "Clienteid",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Clienteid",
                table: "Productos",
                column: "Clienteid");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Usuarios_Clienteid",
                table: "Productos",
                column: "Clienteid",
                principalTable: "Usuarios",
                principalColumn: "id");
        }
    }
}
