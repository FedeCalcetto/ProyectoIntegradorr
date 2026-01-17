using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoIntegrador.EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductos2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "id", "ArtesanoId", "Clienteid", "SubCategoriaId", "descripcion", "imagen", "nombre", "precio", "stock" },
                values: new object[,]
                {
                    { 1, 3, null, 5, "Alfombra tejida a mano con lana natural", "/img/alfombra-textil.jpg", "Alfombra Andina", 3200, 5 },
                    { 2, 3, null, 5, "Manta de algodón tejida a mano", "/img/alfombra-textil.jpg", "Manta Textil Artesanal", 2800, 4 },
                    { 3, 3, null, 8, "Mate artesanal de madera pulida", "/img/mate-madera.jpg", "Mate de Madera Tallado", 1200, 10 },
                    { 4, 3, null, 9, "Caja artesanal de madera natural", "/img/mate-madera.jpg", "Caja Decorativa de Madera", 1500, 6 },
                    { 5, 4, null, 10, "Cartera hecha en cuero natural", "/img/cartera-cuero.jpg", "Cartera de Cuero Premium", 5200, 3 },
                    { 6, 4, null, 11, "Cinturón de cuero genuino", "/img/cartera-cuero.jpg", "Cinturón de Cuero Artesanal", 1800, 8 },
                    { 7, 4, null, 13, "Collar artesanal de plata 925", "/img/collar-plata.jpg", "Collar de Plata", 3900, 4 },
                    { 8, 4, null, 14, "Pulsera de plata hecha a mano", "/img/collar-plata.jpg", "Pulsera Artesanal", 2100, 7 },
                    { 9, 3, null, 1, "Taza de cerámica esmaltada", "/img/taza-ceramica.jpg", "Taza de Cerámica", 900, 12 },
                    { 10, 3, null, 2, "Bowl artesanal de cerámica", "/img/taza-ceramica.jpg", "Bowl de Cerámica", 1300, 6 }
                });

            migrationBuilder.InsertData(
                table: "ProductoFotos",
                columns: new[] { "Id", "ProductoId", "UrlImagen" },
                values: new object[,]
                {
                    { 1, 1, "/img/alfombra-textil.jpg" },
                    { 2, 2, "/img/alfombra-textil.jpg" },
                    { 3, 3, "/img/mate-madera.jpg" },
                    { 4, 4, "/img/mate-madera.jpg" },
                    { 5, 5, "/img/cartera-cuero.jpg" },
                    { 6, 6, "/img/cartera-cuero.jpg" },
                    { 7, 7, "/img/collar-plata.jpg" },
                    { 8, 8, "/img/collar-plata.jpg" },
                    { 9, 9, "/img/taza-ceramica.jpg" },
                    { 10, 10, "/img/taza-ceramica.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductoFotos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "id",
                keyValue: 10);
        }
    }
}
