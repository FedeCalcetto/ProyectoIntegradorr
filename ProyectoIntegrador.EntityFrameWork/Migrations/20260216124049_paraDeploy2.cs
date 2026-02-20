using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoIntegrador.EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class paraDeploy2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 1,
                column: "password",
                value: "$2a$11$TDGzW96H/VB0UcThDKz5V.wAk6dZEDcdtud5YL8O2Dy4wqH7WQceq");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 2,
                column: "password",
                value: "$2a$11$SfZ3q4L2eZcdgFYBAH.85.ODATaXZm56Rgxzh0W4ZhbgP/C9GHG1u");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 3,
                column: "password",
                value: "$2a$11$znZXhvpQIo0TyK71Ip/NYuVcHVv749zm8sDS5q48dheVV8V87PXFq");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 4,
                column: "password",
                value: "$2a$11$ByiDzQypJ1NkuvAokHWCwOUeJi1sBA0.DY0NJNo45J3T6IQh8/eHG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
