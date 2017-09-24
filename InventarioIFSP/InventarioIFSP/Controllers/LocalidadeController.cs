using InventarioIFSP.Database;
using InventarioIFSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioIFSP.Controllers
{
    public class LocalidadeController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Create()
        {
            LocalidadeCategoriaDAO.AtualizaCategorias();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Localidade local)
        {
            try
            {
                if(LocalidadeDAO.Create(local) != null)
                {
                    TempData["msg"] = "Criado com sucesso!";
                    TempData["msg_type"] = "success";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
            }
            TempData["msg"] = "Erro ao criar!";
            TempData["msg_type"] = "danger";
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            try
            {
                LocalidadeCategoriaDAO.AtualizaCategorias();
                return View(LocalidadeDAO.GetAll());
            }
            catch
            {
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                LocalidadeCategoriaDAO.AtualizaCategorias();
                Localidade local = LocalidadeDAO.GetByID(id);
                if (local != null)
                    return View(local);
            }
            catch
            {
            }
            TempData["msg"] = "Erro ao criar!";
            TempData["msg_type"] = "danger";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Localidade local)
        {
            try
            {
                if (LocalidadeDAO.Update(local))
                {
                    TempData["msg"] = "Alterado com sucesso!";
                    TempData["msg_type"] = "success";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
            }
            TempData["msg"] = "Erro ao criar!";
            TempData["msg_type"] = "danger";
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                if (LocalidadeDAO.Delete(id))
                {
                    TempData["msg"] = "Excluído com sucesso!";
                    TempData["msg_type"] = "success";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
            }
            TempData["msg"] = "Erro ao criar!";
            TempData["msg_type"] = "danger";
            return View();
        }
    }
}
