using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.ValueObjects
{
    [Owned]
    public class Direccion
    {
        public string domicilio { get; set; }

        public string departamento { get; set; }

        public string barrio { get; set; }
    }
}
