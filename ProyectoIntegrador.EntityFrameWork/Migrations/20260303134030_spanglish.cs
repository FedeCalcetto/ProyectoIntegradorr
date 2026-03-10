using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoIntegrador.EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class spanglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 1,
                column: "password",
                value: "Admin123456");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 2,
                column: "password",
                value: "Cliente123456");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 3,
                column: "password",
                value: "Artesano123456");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 4,
                column: "password",
                value: "Artesano123456");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 1,
                column: "password",
                value: "$2a$11$HLF8CzmWVtGsxIMl/CS5X.Lwgs/9uEf1H3dNmd38gmu5THsU14xvq");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 2,
                column: "password",
                value: "$2a$11$XZn3d0Nt04GSvoOQRIETluoRe3.nCb5E2URAFWLow3uAtD4URFCiG");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 3,
                column: "password",
                value: "$2a$11$4hAEBgYYHRaqeSMytUygQekLB7vyeM/AgUSZXDtE1m9ZaS8VQXO4u");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 4,
                column: "password",
                value: "$2a$11$px2R0PuzusOQNGii8fkBwODKqdOSZWhyk3iHVcFOCYt/8OZvUacE2");
        }
    }
}
