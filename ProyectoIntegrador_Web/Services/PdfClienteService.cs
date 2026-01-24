using NuGet.Packaging;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using System.Reflection.Metadata;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace ProyectoIntegrador_Web.Services
{
    public class PdfClienteService
    {
        public byte[] Generar(FacturaNoFiscalCliente factura)
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            return QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(20);

                    page.Header().Element(Header);
                    page.Content().Element(c => Content(c, factura));
                    page.Footer().Element(Footer);
                });
            }).GeneratePdf();
        }

        void Header(IContainer container)
        {
            container.PaddingHorizontal(20)
                     .PaddingTop(10)
                     .Background(Colors.Black)
                     .Padding(10)
                     .Text("Factura no fiscal")
                     .FontColor(Colors.White)
                     .FontSize(18)
                     .Bold();
        }

        void Footer(IContainer container)
        {
            container.PaddingHorizontal(20)
             .PaddingBottom(10)
             .Background(Colors.Black)
             .Padding(8)
             .AlignCenter()
             .Column(col =>
             {
                 col.Item().Text("Documento generado automáticamente")
                     .FontColor(Colors.White)
                     .FontSize(9);

                 col.Item().Text(
                     "Factura no fiscal. Documento generado exclusivamente con fines académicos. " +
                     "No posee validez tributaria ni comercial.")
                     .FontColor(Colors.Grey.Lighten3)
                     .FontSize(8);
             });
        }
        void ItemsFactura(IContainer container, FacturaNoFiscalCliente factura)
        {
            container.PaddingTop(10).Column(col =>
            {
                col.Item().Text("Por la compra de:")
                    .Bold()
                    .FontSize(12);

                col.Item().PaddingTop(5).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(3); // Producto
                        columns.RelativeColumn(3); // Artesano
                        columns.RelativeColumn(1); // Precio
                    });

                    // Header
                    table.Header(header =>
                    {
                        header.Cell().Text("Producto").Bold();
                        header.Cell().Text("Artesano").Bold();
                        header.Cell().AlignRight().Text("Precio").Bold();
                    });

                    // Items
                    foreach (var item in factura.itemsFactura ?? new())
                    {
                        table.Cell().Text(item.NombreProducto);
                        table.Cell().Text(item.NombreArtesano);
                        table.Cell().AlignRight().Text($"${item.precioUnitario:0.00}");
                    }
                });
            });
        }

        void Content(IContainer container, FacturaNoFiscalCliente factura)
        {
            container.Padding(10).Column(col =>
            {
                col.Item().Text($"Cliente: {factura.Cliente?.nombre}");
                col.Item().LineHorizontal(1);

                col.Item().Element(c => ItemsFactura(c, factura));

                col.Item().PaddingTop(10).LineHorizontal(1);
                col.Item().Text($"Total: ${factura.Total}")
                    .Bold()
                    .AlignRight();

                col.Item().Text($"Fecha: {factura.Fecha:dd/MM/yyyy}");

                col.Item().PaddingTop(10).Text(
                    "Factura no fiscal. Documento generado exclusivamente con fines académicos. " +
                    "No posee validez tributaria ni comercial.")
                    .FontSize(9)
                    .Italic()
                    .FontColor(Colors.Grey.Darken1);
            });
        }
    } }
