using ProyectoIntegrador.LogicaAplication.Dtos;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador.LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class AgregarUsuarioCasoDeUso : IAgregarUsuario
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public AgregarUsuarioCasoDeUso(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public void Ejecutar(AgregarUsuarioDto dto, string codigoVerificacion)
        {
            Usuario entidad;
            var email = new Email(dto.Email);
            if (dto.EsArtesano)
            {
                entidad = new Artesano
                {
                    nombre = dto.Nombre,
                    apellido = dto.Apellido,
                    email = email,
                    password = dto.Password,
                    rol = "Artesano",
                    CodigoVerificacion = codigoVerificacion,
                    Verificado = false
                };
            }
            else
            {
                entidad = new Cliente
                {
                    nombre = dto.Nombre,
                    apellido = dto.Apellido,
                    email = email,
                    password = dto.Password,
                    rol = "Cliente",
                    CodigoVerificacion = codigoVerificacion,
                    Verificado = false
                };
            }

            _usuarioRepositorio.Agregar(entidad);
        }
    }
}
