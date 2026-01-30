using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoIntegrador.EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class precargaActuliazada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 1,
                column: "imagen",
                value: "alfombra-textil.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 2,
                column: "imagen",
                value: "alfombra-textil.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 3,
                column: "imagen",
                value: "mate-madera.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 4,
                column: "imagen",
                value: "mate-madera.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 5,
                column: "imagen",
                value: "cartera-cuero.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 6,
                column: "imagen",
                value: "cartera-cuero.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 7,
                column: "imagen",
                value: "collar-plata.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 8,
                column: "imagen",
                value: "collar-plata.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 9,
                column: "imagen",
                value: "taza-ceramica.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 10,
                column: "imagen",
                value: "taza-ceramica.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 1,
                column: "imagen",
                value: "/img/alfombra-textil.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 2,
                column: "imagen",
                value: "/img/alfombra-textil.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 3,
                column: "imagen",
                value: "/img/mate-madera.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 4,
                column: "imagen",
                value: "/img/mate-madera.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 5,
                column: "imagen",
                value: "/img/cartera-cuero.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 6,
                column: "imagen",
                value: "/img/cartera-cuero.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 7,
                column: "imagen",
                value: "/img/collar-plata.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 8,
                column: "imagen",
                value: "/img/collar-plata.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 9,
                column: "imagen",
                value: "/img/taza-ceramica.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 10,
                column: "imagen",
                value: "/img/taza-ceramica.jpg");
        }
    }
}
