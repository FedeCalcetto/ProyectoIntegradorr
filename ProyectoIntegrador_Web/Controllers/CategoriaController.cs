using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador_Web.Models;

namespace ProyectoIntegrador_Web.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly IObtenerArtesano _obtenerArtesano;
        private readonly IObtenerCategorias _obtenerCategorias;
        private readonly IObtenerSubcategorias _subCategoria;
        public CategoriaController(IObtenerSubcategorias subCategoria, IObtenerArtesano obtenerArtesano, IObtenerCategorias obtenerCategorias)
        {
            _obtenerArtesano = obtenerArtesano;
            _obtenerCategorias = obtenerCategorias;
            _subCategoria = subCategoria;
        }

        public ActionResult GetSubcategorias(int categoriaId)
        {
            var subcategorias = _subCategoria.obtenerTodas()
        .Where(s => s.categoriaId == categoriaId)
        .Select(s => new {
            id = s.Id,
            nombre = s.Nombre
        });

            return Json(subcategorias);
        }
     
        // GET: CategoriaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CategoriaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoriaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoriaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoriaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
