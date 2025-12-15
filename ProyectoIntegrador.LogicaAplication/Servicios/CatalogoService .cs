using ProyectoIntegrador.LogicaAplication.Dtos;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Servicios
{
    public class CatalogoService : ICatalogoService
    {
        private readonly IProductoRepositorio _productoRepo;
        private readonly ISubCategoriaRepositorio _subCategoriaRepo;

        public CatalogoService(
            IProductoRepositorio productoRepo,
            ISubCategoriaRepositorio subCategoriaRepo)
        {
            _productoRepo = productoRepo;
            _subCategoriaRepo = subCategoriaRepo;
        }

        public CatalogoDto ObtenerCatalogoInicial()
        {
            var dto = new CatalogoDto();

            // 🔹 Recientes
            dto.Recientes = _productoRepo.ObtenerPublicados()
                .OrderByDescending(p => p.id)
                .Take(8)
                .Select(p => new ProductoCatalogoDto
                {
                    Id = p.id,
                    Nombre = p.nombre,
                    Precio = p.precio,
                    Imagen = p.imagen,
                    Artesano = p.artesano.nombre,
                    SubCategoria = p.SubCategoria.Nombre
                })
                .ToList();

            // 🔹 Por SubCategoría
            dto.PorCategoria = _subCategoriaRepo.ObtenerTodas()
            .Where(sc => _productoRepo.ObtenerPublicadosPorSubCategoria(sc.Id).Any()) // 🔹 solo con productos
            .OrderBy(sc => Guid.NewGuid()) //  random
            .Take(3)                       //  máximo 3
            .Select(sc => new CategoriaCatalogoDto
            {
             Nombre = sc.Nombre,
             Productos = _productoRepo.ObtenerPublicadosPorSubCategoria(sc.Id)
                    .Take(4)
                    .Select(p => new ProductoCatalogoDto
                 {
                      Id = p.id,
                     Nombre = p.nombre,
                        Precio = p.precio,
                        Imagen = p.imagen,
                        Artesano = p.artesano.nombre,
                        SubCategoria = sc.Nombre
                 })
                    .ToList()
         })
            .ToList();

            return dto;
        }
    }
}