using ProyectoIntegrador.LogicaAplication.Dtos;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Excepciones;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class CambiarPasswordCasoDeUso : ICambiarPassword
    {
        private readonly IUsuarioRepositorio _repo;

        public CambiarPasswordCasoDeUso(IUsuarioRepositorio repo)
        {
            _repo = repo;
        }
        public void Ejecutar(string password, string passwordRepetida, string passwordActual, string email)
        {
            Usuario u = _repo.BuscarPorEmail(email);
            u.validarContra(password, passwordRepetida, passwordActual);

            u.password = password;

            _repo.Actualizar(u);
        }
    }

}
