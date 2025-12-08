using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Dtos
{
    public class CambiarPasswordDTO
    {
        public string EmailUsuario { get; set; }
        public string PasswordActual { get; set; }
        public string NuevaPassword { get; set; }
        public string RepetirPassword { get; set; }
    }
}
