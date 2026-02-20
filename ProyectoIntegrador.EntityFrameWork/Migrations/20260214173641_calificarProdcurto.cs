using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoIntegrador.EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class calificarProdcurto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productoId = table.Column<int>(type: "int", nullable: false),
                    usuarioId = table.Column<int>(type: "int", nullable: false),
                    puntaje = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_Calificaciones_Productos_productoId",
                        column: x => x.productoId,
                        principalTable: "Productos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_productoId",
                table: "Calificaciones",
                column: "productoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 1,
                column: "password",
                value: "$2a$11$dGiafHYBv1HHMjWsW8gwqeNSdkAyfK1fZhCrTkDove2vSB0B8n1xq");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 2,
                column: "password",
                value: "$2a$11$K5ixKDeqSNI9pSVLo4kOXO9TM01tFU1IkkEj8nZrxw16g84zWQDWW");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 3,
                column: "password",
                value: "$2a$11$me74bJ59LYdMNL7GeQVfXuv8vE/Rgq3zBgisp.SITnSZnjSVoJ5J.");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 4,
                column: "password",
                value: "$2a$11$t0k937WfkX3O5el00.IyGO25LUQKMG4YROB3TRVMs/9dK51p86LcS");
        }
    }
}
