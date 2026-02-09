using ProyectoIntegrador.LogicaAplication.Dtos;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class DashboardCasoDeUso : IDashboard
    {
        private readonly IArtesanoRepositorio _ArtesanoRepo;

        public DashboardCasoDeUso(IArtesanoRepositorio artesanoRepo)
        {
            _ArtesanoRepo = artesanoRepo;
        }

        public DashboardDto Ejecutar(string emailArtesano, int cantidadMeses = 12)
        {
            var artesano = _ArtesanoRepo.ObtenerArtesanoDashboard(emailArtesano);

            DateTime hoy = DateTime.UtcNow;
            DateTime inicioPeriodo = new DateTime(hoy.Year, hoy.Month, 1)
                                        .AddMonths(-cantidadMeses + 1);

            var topVentas = artesano.ventas
                .SelectMany(v => v.Orden.Items)
                .Where(i => i.ArtesanoId == artesano.id)
                .GroupBy(i => i.ProductoId)
                .Select(g =>
                {
                    var producto = artesano.productos
                        .FirstOrDefault(p => p.id == g.Key);

                    return new TopVentasDTO
                    {
                        ProductoId = g.Key,
                        NombreProducto = producto != null
                            ? producto.nombre
                            : "Producto eliminado",
                        CantidadVendida = g.Sum(i => i.Cantidad)
                    };
                })
                .OrderByDescending(x => x.CantidadVendida)
                .Take(3)
                .ToList();

            var ventasUltimosMeses = artesano.ventas
                .Where(v =>
                    v.Fecha >= inicioPeriodo &&
                    v.Fecha <= hoy
                )
                .GroupBy(v => new { v.Fecha.Year, v.Fecha.Month })
                .Select(g => new VentasPorMesDto
                {
                    Ano = g.Key.Year,
                    Mes = g.Key.Month,
                    Cantidad = g.Count()
                })
                .OrderBy(x => x.Ano)
                .ThenBy(x => x.Mes)
                .ToList();

            var dashboard = new DashboardDto
            {
                CantidadVentasTotal = artesano.Totalventas(),
                CantidadVentasMesActual = artesano.VentasMesActual(),
                CantidadVentasAnoActual = artesano.TotalventasXAno(),
                VariacionVentasMensual = artesano.VariacionVentasMensual(),
                GraficoVentas = ventasUltimosMeses,
                TopProductosVentas = topVentas
            };

            return dashboard;
        }
    }
}

