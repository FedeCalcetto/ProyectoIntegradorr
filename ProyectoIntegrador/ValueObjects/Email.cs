using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.ValueObjects
{
    [Owned]
    public class Email
    {
        public string email { get; set; }
    }
}
