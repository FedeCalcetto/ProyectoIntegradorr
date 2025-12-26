using ProyectoIntegrador.EntityFrameWork.Repositorios;
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
    public class AgregarReporteCasoDeUso: IAgregarReporte
    {
        private readonly IReporteRepositorio agregarReporte;

        public AgregarReporteCasoDeUso(IReporteRepositorio _agregarReporte)
        {
            agregarReporte = _agregarReporte;
        }

        public void Ejecutar(AgregarReporteDto dto, Artesano artesano, Cliente cliente, Producto producto)
        {
            if (artesano == null && producto == null)
                throw new ArgumentException("Debe indicarse un artesano o un producto.");

            if (artesano != null && producto != null)
                throw new ArgumentException("Solo se puede reportar un artesano o un producto.");

            var reporte = new Reporte
            {
                razon = dto.razon,
                cliente = cliente,
                clienteId = cliente.id,
                fecha = DateTime.UtcNow
            };

            if (producto != null)
            {
                reporte.producto = producto;
                reporte.productoId = producto.id;
            }

            if (artesano != null)
            {
                reporte.artesano = artesano;
                reporte.artesanoId = artesano.id;
            }

            agregarReporte.Agregar(reporte);

        }
    }
}
