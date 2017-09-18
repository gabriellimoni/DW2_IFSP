using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioIFSP.Controllers
{
    public class SalaCategoriaController : Controller
    {
        // GET: SalaCategoria
        public ActionResult Index()
        {
            return View();
        }

        // GET: SalaCategoria/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SalaCategoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalaCategoria/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SalaCategoria/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SalaCategoria/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SalaCategoria/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SalaCategoria/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
