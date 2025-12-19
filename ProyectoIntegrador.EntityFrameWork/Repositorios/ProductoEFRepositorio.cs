using Microsoft.EntityFrameworkCore;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Excepciones;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.EntityFrameWork.Repositorios
{
    public class ProductoEFRepositorio : IProductoRepositorio
    {

        private readonly ProyectoDBContext _contexto;

        public ProductoEFRepositorio(ProyectoDBContext contexto)
        {
            _contexto = contexto;
        }
        public void Agregar(Producto entidad)
        {
            entidad.Validar();
            _contexto.Productos.Add(entidad);
            _contexto.SaveChanges();
        }

        public void Editar(Producto entidad)
        {
            var existente = _contexto.Productos
           .Include(p => p.Fotos)
           .FirstOrDefault(p => p.id == entidad.id);

            if (existente == null)
                throw new Exception("Producto no encontrado");
            existente.nombre = entidad.nombre;
            existente.descripcion = entidad.descripcion;
            existente.precio = entidad.precio;
            existente.stock = entidad.stock;
            existente.SubCategoriaId = entidad.SubCategoriaId;
            existente.imagen = entidad.imagen;

            // ------------------------------
            // ACTUALIZAR LISTA DE FOTOS
            // ------------------------------

            if (existente.Fotos != null && entidad.Fotos.Count > 0)
            {
                existente.Fotos.Clear();
                foreach (var foto in entidad.Fotos)
                {
                    existente.Fotos.Add(new ProductoFoto
                    {
                        UrlImagen = foto.UrlImagen,
                        ProductoId = existente.id
                    });
                }
            }
            _contexto.SaveChanges();


        }

        public void Editar(Producto producto, List<string> fotos)
        {

            var existente = _contexto.Productos
           .Include(p => p.Fotos)
           .FirstOrDefault(p => p.id == producto.id);
            if (existente == null)
                throw new Exception("Producto no encontrado");
            existente.nombre = producto.nombre;
            existente.descripcion = producto.descripcion;
            existente.precio = producto.precio;
            existente.stock = producto.stock;
            existente.SubCategoriaId = producto.SubCategoriaId;
            existente.imagen = producto.imagen;

            // ------------------------------
            // ACTUALIZAR LISTA DE FOTOS
            // ------------------------------

            if (existente.Fotos == null)
                existente.Fotos = new List<ProductoFoto>();

            existente.Fotos.Clear();
            foreach (var url in fotos)
            {
                existente.Fotos.Add(new ProductoFoto
                {
                    UrlImagen = url,
                    ProductoId = existente.id
                });
            }
            fotos.Clear();
            _contexto.SaveChanges();
        }

        public void Eliminar(int id)
        {

            var productoDominio = Obtener(id);

            if(productoDominio is null) {
                throw new ProductoNoEncontradoException();
            }

            _contexto.Productos.Remove(productoDominio);
            _contexto.SaveChanges();
        }

        public Producto Obtener(int Id)
        {
            return _contexto.Productos
            .Include(x => x.Fotos)
            .Include(p => p.artesano)
            .Include(p => p.SubCategoria)
            .FirstOrDefault(x => x.id == Id);
        }

        public IEnumerable<Producto> ObtenerPublicados()
        {
            return _contexto.Productos
        .Include(p => p.artesano)
        .Include(p => p.SubCategoria)
        .Where(p => p.stock > 0)
        .ToList();
        }

        public IEnumerable<Producto> ObtenerPublicadosPorSubCategoria(int subCategoriaId)
        {
            return _contexto.Productos
        .Include(p => p.artesano)
        .Include(p => p.SubCategoria)
        .Where(p => p.SubCategoriaId == subCategoriaId && p.stock > 0)
        .ToList();
        }
        public List<Producto> ObtenerProductosExcluyendo(List<int> idsEnCarrito, int maxItems)
        {
            return _contexto.Productos
                    .Where(p => !idsEnCarrito.Contains(p.id))
                    .Take(maxItems)
                    .ToList();
        }

        public IEnumerable<Producto> ObtenerTodos()
        {
            return _contexto.Productos;
         }

        public List<Producto> ProductosFiltrados(string filtro,int? precioMin, int? precioMax,int pagina, int tamanoPagina,out int totalRegistros,
                                                  int? categoriaId, int? subCategoriaId)
        {

            if (pagina < 1)
                pagina = 1;

            var query = _contexto.Productos
            .Include(p => p.SubCategoria)
            .AsQueryable();

            // Filtro por texto
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                query = query.Where(p => p.nombre.Contains(filtro));
            }

            // Normalizar valores negativos
            int? min = (precioMin.HasValue && precioMin >= 0) ? precioMin : null;
            int? max = (precioMax.HasValue && precioMax >= 0) ? precioMax : null;

            // Invertir si min > max
            if (min.HasValue && max.HasValue && min > max)
            {
                (min, max) = (max, min);
            }

            // Filtros por precio
            if (min.HasValue)
                query = query.Where(p => p.precio >= min.Value);

            if (max.HasValue)
                query = query.Where(p => p.precio <= max.Value);

            if (subCategoriaId.HasValue)
            {
                query = query.Where(p => p.SubCategoriaId == subCategoriaId.Value);
            }
            else if (categoriaId.HasValue)
            {
                query = query.Where(p => p.SubCategoria.categoriaId == categoriaId.Value);
            }
            // Total antes de paginar
            totalRegistros = query.Count();

            // Orden fijo para paginación estable
            query = query.OrderBy(p => p.nombre);

            // Paginación
            return query
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();
        }
    }
}
