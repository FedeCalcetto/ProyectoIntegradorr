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
    public class AgregarProductoCasoDeUso : IAgregarProducto
    {

        private readonly IProductoRepositorio _agregarProducto;

        public AgregarProductoCasoDeUso(IProductoRepositorio agregarProducto)
        {
            _agregarProducto = agregarProducto;
        }
        public void Ejecutar(AgregarProductoDto dto, Artesano a)
        {
            if (a == null)
                throw new Exception("Artesano inválido");

            var producto = new Producto
            {
                nombre = dto.Nombre,
                descripcion = dto.Descripcion,
                precio = dto.Precio,
                stock = dto.Stock,
                imagen = dto.Imagen,
                SubCategoriaId = dto.SubCategoriaId,
                artesano = a
            };

            _agregarProducto.Agregar(producto);

        }
    }
}
