using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoIntegrador.EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class pro : Migration
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

            migrationBuilder.DropColumn(
                name: "MercadoPagoPaymentIds",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "PagosAprobados",
                table: "Ordenes");

            migrationBuilder.AddColumn<long>(
                name: "MercadoPagoPaymentId",
                table: "Ordenes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MercadoPagoPaymentId",
                table: "Ordenes");

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

            migrationBuilder.AddColumn<string>(
                name: "MercadoPagoPaymentIds",
                table: "Ordenes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PagosAprobados",
                table: "Ordenes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
