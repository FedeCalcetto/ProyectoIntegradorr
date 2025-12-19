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
    public class ObtenerSubCategoriaCasoDeUso : IObtenerSubCategoria
    {
        private readonly ISubCategoriaRepositorio _subCategoriaRepositorio;

        public ObtenerSubCategoriaCasoDeUso(ISubCategoriaRepositorio subCategoriaRepositorio)
        {
            _subCategoriaRepositorio = subCategoriaRepositorio;
        }
        public IEnumerable<SubCategoria> obtenerPorCtegoria(int categoriaId)
        {
            return _subCategoriaRepositorio.ObtenerPorCategoria(categoriaId);
        }
    }
}
