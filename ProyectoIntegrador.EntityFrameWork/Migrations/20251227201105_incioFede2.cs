using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoIntegrador.EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class incioFede2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reportes_Productos_productoId",
                table: "Reportes");

            migrationBuilder.AddForeignKey(
                name: "FK_Reportes_Productos_productoId",
                table: "Reportes",
                column: "productoId",
                principalTable: "Productos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reportes_Productos_productoId",
                table: "Reportes");

            migrationBuilder.AddForeignKey(
                name: "FK_Reportes_Productos_productoId",
                table: "Reportes",
                column: "productoId",
                principalTable: "Productos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
