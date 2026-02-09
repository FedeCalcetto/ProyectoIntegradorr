using Microsoft.AspNetCore.Mvc;
using ProyectoIntegrador.LogicaAplication.Interface;

public class CarritoCantidadViewComponent : ViewComponent
{
    private readonly IMostrarProductosCarrito _mostrarProductosCarrito;
    private readonly IObtenerUsuario _obtenerUsuario;

    public CarritoCantidadViewComponent(IMostrarProductosCarrito mostrarProductosCarrito, IObtenerUsuario obtenerUsuario)
    {
        _mostrarProductosCarrito = mostrarProductosCarrito;
        _obtenerUsuario = obtenerUsuario;
    }

    public IViewComponentResult Invoke()
    {
        var email = HttpContext.Session.GetString("loginUsuario");
        var usuario = _obtenerUsuario.Ejecutar(email);
        var items = _mostrarProductosCarrito.mostrarProductos(usuario.id);

        var cantidadTotal = items.Count();

        return View(cantidadTotal);
    }
}
