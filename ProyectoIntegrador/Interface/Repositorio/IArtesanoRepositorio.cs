using ProyectoIntegrador.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Interface.Repositorio
{
    public interface IArtesanoRepositorio : IRepositorio<Artesano>
    {
        Artesano ObtenerPorEmail(string email);
        void Actualizar(Artesano artesano);
        Artesano ObtenerProductosArtesano(string email);
        void bloquearArtesano(int id);
        Artesano ObtenerArtesanoDashboard(string email);
    }
}
