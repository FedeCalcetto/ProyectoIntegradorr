using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.ValueObjects
{
    [Owned]
    public class Email
    {
        public string email { get; set; }

        public Email(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor) || !valor.Contains("@"))
                throw new Exception("Email inválido.");

            email = valor;
        }

        private Email() { }
    }
}
