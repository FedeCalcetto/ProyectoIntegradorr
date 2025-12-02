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
            entidad.Validar();

            var entidadDominio = Obtener(entidad.id);

            if (entidadDominio is null)
            {
                throw new ProductoNoEncontradoException();
            }

            entidadDominio.nombre = entidad.nombre;
            entidadDominio.descripcion = entidad.descripcion;
            entidadDominio.precio = entidad.precio;
            entidadDominio.stock = entidad.stock;
            _contexto.Update(entidadDominio);
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
            .FirstOrDefault(x => x.id == Id);
        }

        public IEnumerable<Producto> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
