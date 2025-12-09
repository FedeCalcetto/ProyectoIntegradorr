using ProyectoIntegrador.LogicaAplication.Dtos;
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
    public class EditarProductoCasoDeUso : IEditarProducto
    {

        private readonly IProductoRepositorio _productoRepositorio;

        public EditarProductoCasoDeUso(IProductoRepositorio productoRepositorio)
        {
            _productoRepositorio = productoRepositorio;
        }

        public void Ejecutar(EditarProductoDto dto)
        {
            var producto = _productoRepositorio.Obtener(dto.Id);

            if (producto == null)
                throw new Exception("Producto no encontrado");

            producto.nombre = dto.Nombre;
            producto.descripcion = dto.Descripcion;
            producto.precio = dto.Precio;
            producto.stock = dto.Stock;
            producto.SubCategoriaId = dto.SubCategoriaId;
            producto.imagen = dto.ImagenPrincipal;
            //producto.Fotos = dto.Fotos
            //.Select(x => new ProductoFoto { UrlImagen = x })
            //.ToList();

            _productoRepositorio.Editar(producto, dto.Fotos);
        }
    }
}
