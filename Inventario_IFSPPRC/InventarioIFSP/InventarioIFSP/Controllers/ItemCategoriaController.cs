using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioIFSP.Controllers
{
    public class ItemCategoriaController : Controller
    {
        // GET: ItemCategoria
        public ActionResult Index()
        {
            return View();
        }

        // GET: ItemCategoria/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ItemCategoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemCategoria/Create
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

        // GET: ItemCategoria/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ItemCategoria/Edit/5
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

        // GET: ItemCategoria/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ItemCategoria/Delete/5
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
