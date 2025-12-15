using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Dtos
{
    public class EditarArtesanoDto
    {
        public string Descripcion { get; set; }
        public string Telefono { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Foto { get; set; }
    }
}
