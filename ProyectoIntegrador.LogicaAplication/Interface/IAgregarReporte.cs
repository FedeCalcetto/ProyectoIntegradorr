using ProyectoIntegrador.LogicaAplication.Dtos;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Interface
{
    public interface IAgregarReporte
    {
        void Ejecutar(AgregarReporteDto dto, Artesano artesano,Cliente cliente, Producto producto);
    }
}
