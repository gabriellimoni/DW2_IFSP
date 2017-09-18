using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioIFSP.Controllers
{
    public class ItemSubCategoriaController : Controller
    {
        // GET: ItemSubCategoria
        public ActionResult Index()
        {
            return View();
        }

        // GET: ItemSubCategoria/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ItemSubCategoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemSubCategoria/Create
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

        // GET: ItemSubCategoria/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ItemSubCategoria/Edit/5
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

        // GET: ItemSubCategoria/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ItemSubCategoria/Delete/5
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
