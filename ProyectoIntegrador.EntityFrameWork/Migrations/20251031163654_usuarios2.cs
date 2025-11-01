using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoIntegrador.EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class usuarios2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "id", "TipoUusario", "apellido", "nombre", "password", "email_email", "direccion_barrio", "direccion_departamento", "direccion_domicilio" },
                values: new object[] { 1, "Cliente", "Pérez", "Juan", "juancliente123", "juan@cliente.com", "Centro", "Montevideo", "Av. Libertad 123" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "id", "Clienteid", "TipoUusario", "apellido", "descripcion", "foto", "nombre", "password", "telefono", "email_email" },
                values: new object[] { 2, null, "Artesano", "Gómez", "Artesana especializada en cerámica artesanal.", "laura.jpg", "Laura", "lauraartesana123", "099123456", "laura@artesana.com" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "id", "TipoUusario", "apellido", "nombre", "password", "email_email" },
                values: new object[] { 3, "Admin", "Root", "Admin", "admin123456", "admin@site.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
