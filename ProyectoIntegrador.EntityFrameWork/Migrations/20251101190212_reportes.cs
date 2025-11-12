using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoIntegrador.EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class reportes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "artesanoReportado",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "productoReportado",
                table: "Comentarios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "artesanoReportado",
                table: "Comentarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "productoReportado",
                table: "Comentarios",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
