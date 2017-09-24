﻿using InventarioIFSP.Database;
using InventarioIFSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioIFSP.Controllers
{
    public class ItemStatusController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ItemStatus cat)
        {
            try
            {
                if (ItemStatusDAO.Create(cat) != null)
                {
                    TempData["msg"] = "Criado com sucesso!";
                    TempData["msg_type"] = "success";
                    return RedirectToAction("Index");

                }

            }
            catch
            {
            }
            TempData["msg"] = "Erro ao criar";
            TempData["msg_type"] = "danger";
            return View();
        }

        public ActionResult Edit(int id)
        {
            try
            {
                ItemStatus cat = ItemStatusDAO.GetByID(id);
                if (cat != null)
                    return View(cat);
            }
            catch
            {
            }
            TempData["msg"] = "Erro ao buscar dados";
            TempData["msg_type"] = "danger";
            return View();
        }

        [HttpPost]
        public ActionResult Edit(ItemStatus status)
        {
            try
            {
                if (ItemStatusDAO.Update(status))
                {
                    TempData["msg"] = "Alterado com sucesso!";
                    TempData["msg_type"] = "success";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
            }
            TempData["msg"] = "Erro ao criar";
            TempData["msg_type"] = "danger";
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                if (ItemStatusDAO.Delete(id))
                {
                    TempData["msg"] = "Excluído com sucesso!";
                    TempData["msg_type"] = "success";
                    return RedirectToAction("List");
                }
            }
            catch
            {
            }
            TempData["msg"] = "Erro ao excluir";
            TempData["msg_type"] = "danger";
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            try
            {
                return View(ItemStatusDAO.GetAll());
            }
            catch
            {
                return View();
            }
        }
    }
}
