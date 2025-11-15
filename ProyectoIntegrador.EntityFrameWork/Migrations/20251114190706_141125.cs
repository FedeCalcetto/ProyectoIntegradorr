using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoIntegrador.EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class _141125 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "direccion_barrio", "direccion_domicilio" },
                values: new object[] { "Centro", "Calle 123" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "foto", "direccion_barrio", "direccion_departamento", "direccion_domicilio" },
                values: new object[] { null, "Centro", "Montevideo", "Calle 123" });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "foto", "direccion_barrio", "direccion_domicilio" },
                values: new object[] { null, "Centro2", "Calle 1234" });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "foto", "direccion_barrio", "direccion_departamento", "direccion_domicilio" },
                values: new object[] { null, "Centro3", "Montevideo", "Calle 12345" });
        }
    }
}
