using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaNegocio.Interface
{
    public interface IRepositorio<T>
    {

        T Obtener(int id);
        IEnumerable<T> ObtenerTodos();
        void Agregar(T entidad);
        void Editar(T entidad);
        void Eliminar(int id);



    }
}
