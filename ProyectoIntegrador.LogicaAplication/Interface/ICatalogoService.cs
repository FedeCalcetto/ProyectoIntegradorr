using ProyectoIntegrador.LogicaAplication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Interface
{
    public interface ICatalogoService
    {
       // CatalogoDto ObtenerCatalogoInicial(int cantidadRecientes = 8, int cantidadPorCategoria = 4);
        CatalogoDto ObtenerCatalogoInicial();
    }
}
