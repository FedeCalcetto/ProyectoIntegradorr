using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoIntegrador.EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class AddTokenVerificacionEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TokenVerificacionEmail",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenVerificacionEmailExpira",
                table: "Usuarios",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "TokenVerificacionEmail", "TokenVerificacionEmailExpira" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "TokenVerificacionEmail", "TokenVerificacionEmailExpira" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "TokenVerificacionEmail", "TokenVerificacionEmailExpira" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenVerificacionEmail",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "TokenVerificacionEmailExpira",
                table: "Usuarios");
        }
    }
}
