using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoIntegrador_Web.Controllers
{
    public class ArtesanoController : Controller
    {
        // GET: ArtesanoController
        public ActionResult Inicio()
        {
            if (HttpContext.Session.GetString("Rol") != null && HttpContext.Session.GetString("Rol").Trim().ToUpper().Equals("ARTESANO"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: ArtesanoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ArtesanoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtesanoController/Create
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

        // GET: ArtesanoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ArtesanoController/Edit/5
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

        // GET: ArtesanoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ArtesanoController/Delete/5
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
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
