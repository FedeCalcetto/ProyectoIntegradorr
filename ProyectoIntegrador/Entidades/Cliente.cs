using Microsoft.IdentityModel.Tokens;
using ProyectoIntegrador.LogicaNegocio.Excepciones;
using ProyectoIntegrador.LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Entidades
{
    public class Cliente : Usuario
    {

        public List<Artesano> artesanosSeguidos { get; set; }
        public List<Factura> compras { get; set; } = new List<Factura>();
        public List<Producto> productosFavoritos { get; set; }
        //public List<PedidoPersonalizado> pedidosCliente { get; set; }
        public Direccion? direccion { get; set; }
        public string? foto { get; set; }
        public void validarEditar()
        {
            valdiarDireccion();
        }

        private void valdiarDireccion()
        {
            if (direccion.barrio.IsNullOrEmpty() || direccion.departamento.IsNullOrEmpty() || direccion.domicilio.IsNullOrEmpty())
            {
                throw new DireccionException();
            }
        }
        
        public void validarContra(string contraNueva,string contraRepetida,string contraActual)
        {
            ValidarContraseñaLongitud(contraNueva, contraRepetida);
            ValidarPasswordMaysucula(contraNueva, contraRepetida);
            ValidarNumero(contraNueva, contraRepetida);
            SonIguales(contraNueva,contraRepetida,contraActual);
        }

        private void ValidarContraseñaLongitud(string contraNueva, string contraRepetida)
        {
            if (contraNueva.Length < 10 || contraNueva.Length > 30 || contraRepetida.Length < 10 || contraRepetida.Length > 30)
            {
                throw new passwordUsuarioException();
            }
        }

        private void ValidarPasswordMaysucula(string contraNueva, string contraRepetida)
        {
            if (!contraNueva.Any(char.IsUpper) || !contraNueva.Any(char.IsLower) || !contraRepetida.Any(char.IsUpper) || !contraRepetida.Any(char.IsUpper))
            {
                throw new MayusculaPasswordException();
            }

        }

        private void ValidarNumero(string contraNueva, string contraRepetida)
        {
            if (!contraNueva.Any(char.IsDigit) || !contraRepetida.Any(char.IsDigit))
            {
                throw new numeroPassowordException();
            }
        }

        private void SonIguales(string contraNueva, string contraRepetida, string contraActual)
        {
            if (!contraActual.Equals(password))
            {
                throw new ContraActualException();
            }


            if(contraNueva.Equals(password))
            {
                 throw new SonIgualesException();
            }

            if (!contraNueva.Equals(contraRepetida))
            {
                throw new NoCoincideException();
            }
        }
        
    }
}
