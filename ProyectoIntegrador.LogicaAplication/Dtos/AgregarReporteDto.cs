using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Dtos
{
    public class AgregarReporteDto
    {
        [Required(ErrorMessage = "Debe ingresar un motivo para el reporte")]
        public string razon { get; set; }
    }
}
