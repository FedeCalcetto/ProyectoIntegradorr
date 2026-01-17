using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;

namespace ProyectoIntegrador_Web.Controllers
{
    public class OrdenController : Controller
    {
        private readonly IOrdenRepositorio _ordenRepo;

        public OrdenController(IOrdenRepositorio ordenRepo)
        {
            _ordenRepo = ordenRepo;
        }

        //[HttpGet]
        //public async Task<IActionResult> OrdenesPendientes(string ids)
        //{
        //    var guids = ids.Split(',')
        //                   .Select(Guid.Parse)
        //                   .ToList();

        //    var ordenes = await _ordenRepo.ObtenerPorIdsAsync(guids);

        //    return View(ordenes);
        //}
    }

}
