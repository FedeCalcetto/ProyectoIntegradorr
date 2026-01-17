using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoIntegrador.EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class inicioFede2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Artesano_bloqueado",
                table: "Usuarios",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "bloqueado",
                table: "Usuarios",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Artesano_bloqueado",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "bloqueado",
                table: "Usuarios");
        }
    }
}
