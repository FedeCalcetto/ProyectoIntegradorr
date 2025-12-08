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
    public class ObtenerCategoriasCasoDeUso : IObtenerCategorias
    {
        private readonly ICategoriaRepositorio _repo;

        public ObtenerCategoriasCasoDeUso(ICategoriaRepositorio repo)
        {
            _repo = repo;
        }
        public IEnumerable<Categoria> ObtenerTodos()
        {
            return _repo.ObtenerTodos();
        }
    }
}
