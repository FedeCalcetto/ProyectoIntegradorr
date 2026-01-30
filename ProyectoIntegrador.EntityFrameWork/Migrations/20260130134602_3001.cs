using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoIntegrador.EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class _3001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MercadoPagoAccessToken",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "MercadoPagoRefreshToken",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "MercadoPagoTokenExpira",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "MercadoPagoUserId",
                table: "Usuarios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MercadoPagoAccessToken",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MercadoPagoRefreshToken",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MercadoPagoTokenExpira",
                table: "Usuarios",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MercadoPagoUserId",
                table: "Usuarios",
                type: "bigint",
                nullable: true);
        }
    }
}
