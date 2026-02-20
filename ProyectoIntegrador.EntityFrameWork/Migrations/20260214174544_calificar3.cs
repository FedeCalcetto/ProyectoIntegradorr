using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoIntegrador.EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class calificar3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Calificaciones",
                columns: new[] { "id", "fecha", "productoId", "puntaje", "usuarioId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5m, 2 },
                    { 2, new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4m, 2 },
                    { 3, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3m, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Calificaciones",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Calificaciones",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Calificaciones",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
