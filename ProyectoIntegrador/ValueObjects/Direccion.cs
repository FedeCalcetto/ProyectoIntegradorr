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
        public Direccion(string domicilio, string departamento, string barrio)
        {
            this.domicilio = domicilio;
            this.departamento = departamento;
            this.barrio = barrio;
        }

        public string domicilio { get; set; }

        public string departamento { get; set; }

        public string barrio { get; set; }
    }
}
