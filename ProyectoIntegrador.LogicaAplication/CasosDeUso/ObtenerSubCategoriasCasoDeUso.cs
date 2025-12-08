using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class ObtenerSubCategoriasCasoDeUso : IObtenerSubcategorias
    {
        private readonly ISubCategoriaRepositorio _subCategoria;


        public ObtenerSubCategoriasCasoDeUso(ISubCategoriaRepositorio subCategoria)
        {
            _subCategoria = subCategoria;
        }
        public IEnumerable<SubCategoria> obtenerTodas()
        {
            return _subCategoria.ObtenerTodos();
        }
    }
}
