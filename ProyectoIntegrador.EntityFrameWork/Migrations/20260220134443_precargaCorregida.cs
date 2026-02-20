using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoIntegrador.EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class precargaCorregida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 1,
                column: "UrlImagen",
                value: "alfombra-textil.jpg");

            migrationBuilder.UpdateData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 2,
                column: "UrlImagen",
                value: "alfombra-textil.jpg");

            migrationBuilder.UpdateData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 3,
                column: "UrlImagen",
                value: "mate-madera.jpg");

            migrationBuilder.UpdateData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 4,
                column: "UrlImagen",
                value: "mate-madera.jpg");

            migrationBuilder.UpdateData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 5,
                column: "UrlImagen",
                value: "cartera-cuero.jpg");

            migrationBuilder.UpdateData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 6,
                column: "UrlImagen",
                value: "cartera-cuero.jpg");

            migrationBuilder.UpdateData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 7,
                column: "UrlImagen",
                value: "collar-plata.jpg");

            migrationBuilder.UpdateData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 8,
                column: "UrlImagen",
                value: "collar-plata.jpg");

            migrationBuilder.UpdateData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 9,
                column: "UrlImagen",
                value: "taza-ceramica.jpg");

            migrationBuilder.UpdateData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 10,
                column: "UrlImagen",
                value: "taza-ceramica.jpg");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 1,
                column: "password",
                value: "$2a$11$SoI3uk3q0Lo2g61olEKWue1G9VkkIfTMvB7.4OKUGGEc6/Cw8/hpS");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 2,
                column: "password",
                value: "$2a$11$YuD9kzAyuwY/POvqFq1Azueq7leolpMKyJ.2oD10z7rSSwgjCL/AG");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 3,
                column: "password",
                value: "$2a$11$yykxM1/15bM.Zvc0gaYxL.eXv9CCVdqRSqx.Z78iiryEW4dVhlJZ.");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 4,
                column: "password",
                value: "$2a$11$VuWSOhwrYFSba2fTwikyo.KASzk4Mel9pyu4y880yyIYYY8oP7GWe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 1,
                column: "UrlImagen",
                value: "/img/alfombra-textil.jpg");

            migrationBuilder.UpdateData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 2,
                column: "UrlImagen",
                value: "/img/alfombra-textil.jpg");

            migrationBuilder.UpdateData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 3,
                column: "UrlImagen",
                value: "/img/mate-madera.jpg");

            migrationBuilder.UpdateData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 4,
                column: "UrlImagen",
                value: "/img/mate-madera.jpg");

            migrationBuilder.UpdateData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 5,
                column: "UrlImagen",
                value: "/img/cartera-cuero.jpg");

            migrationBuilder.UpdateData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 6,
                column: "UrlImagen",
                value: "/img/cartera-cuero.jpg");

            migrationBuilder.UpdateData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 7,
                column: "UrlImagen",
                value: "/img/collar-plata.jpg");

            migrationBuilder.UpdateData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 8,
                column: "UrlImagen",
                value: "/img/collar-plata.jpg");

            migrationBuilder.UpdateData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 9,
                column: "UrlImagen",
                value: "/img/taza-ceramica.jpg");

            migrationBuilder.UpdateData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 10,
                column: "UrlImagen",
                value: "/img/taza-ceramica.jpg");

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
    }
}
