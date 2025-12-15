using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Dtos
{
    public class EditarClienteDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Domicilio { get; set; }
        public string Departamento { get; set; }
        public string Barrio { get; set; }
        public List<string>? DepartamentosOpciones { get; set; }
        public string? Foto { get; set; }
    }
}
