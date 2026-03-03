using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Interface
{
    public interface IConfirmarResetPassword
    {
        void Ejecutar(string token, string nueva, string repetir);
    }
}
