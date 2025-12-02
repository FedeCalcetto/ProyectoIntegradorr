using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoIntegrador.EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class _02122025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Usuarios_Clienteid",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Usuarios_Clienteid",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Clienteid",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Productos_Clienteid",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Clienteid",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Clienteid",
                table: "Productos");

            migrationBuilder.CreateTable(
                name: "ClienteArtesanoSeguido",
                columns: table => new
                {
                    Clienteid = table.Column<int>(type: "int", nullable: false),
                    artesanosSeguidosid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteArtesanoSeguido", x => new { x.Clienteid, x.artesanosSeguidosid });
                    table.ForeignKey(
                        name: "FK_ClienteArtesanoSeguido_Usuarios_Clienteid",
                        column: x => x.Clienteid,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClienteArtesanoSeguido_Usuarios_artesanosSeguidosid",
                        column: x => x.artesanosSeguidosid,
                        principalTable: "Usuarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ClienteProductoFavorito",
                columns: table => new
                {
                    Clienteid = table.Column<int>(type: "int", nullable: false),
                    productosFavoritosid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteProductoFavorito", x => new { x.Clienteid, x.productosFavoritosid });
                    table.ForeignKey(
                        name: "FK_ClienteProductoFavorito_Productos_productosFavoritosid",
                        column: x => x.productosFavoritosid,
                        principalTable: "Productos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClienteProductoFavorito_Usuarios_Clienteid",
                        column: x => x.Clienteid,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClienteArtesanoSeguido_artesanosSeguidosid",
                table: "ClienteArtesanoSeguido",
                column: "artesanosSeguidosid");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteProductoFavorito_productosFavoritosid",
                table: "ClienteProductoFavorito",
                column: "productosFavoritosid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteArtesanoSeguido");

            migrationBuilder.DropTable(
                name: "ClienteProductoFavorito");

            migrationBuilder.AddColumn<int>(
                name: "Clienteid",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Clienteid",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Clienteid",
                table: "Usuarios",
                column: "Clienteid");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Usuarios_Clienteid",
                table: "Usuarios",
                column: "Clienteid",
                principalTable: "Usuarios",
                principalColumn: "id");
        }
    }
}
