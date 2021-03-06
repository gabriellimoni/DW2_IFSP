﻿using InventarioIFSP.Database;
using InventarioIFSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioIFSP.Controllers
{
    public class ItemCategoriaController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        
        public ActionResult Create()
        {
            LocalidadeCategoriaDAO.AtualizaCategorias();
            return View();
        }

        [HttpPost]
        public ActionResult Create(ItemCategoria cat)
        {
            // Início: Proteção de rota
            if (Session["usuario_nivel"] == null) 
            {
                TempData["msg"] = "Necessário estar logado!";
                TempData["msg_type"] = "warning";
                return RedirectToAction("Index", "Home");
            }

            if (Convert.ToInt32(Session["usuario_nivel"]) > 1)
            {
                TempData["msg"] = "Você não tem autorização para acessar esta área!";
                TempData["msg_type"] = "danger";
                return RedirectToAction("Index", "Home");
            }
            // Fim: Proteção de Rota

            try
            {
                if(ItemCategoriaDAO.Create(cat) != null)
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
            // Início: Proteção de rota
            if (Session["usuario_nivel"] == null)
            {
                TempData["msg"] = "Necessário estar logado!";
                TempData["msg_type"] = "warning";
                return RedirectToAction("Index", "Home");
            }

            if (Convert.ToInt32(Session["usuario_nivel"]) > 1)
            {
                TempData["msg"] = "Você não tem autorização para acessar esta área!";
                TempData["msg_type"] = "danger";
                return RedirectToAction("Index", "Home");
            }
            // Fim: Proteção de Rota

            try
            {
                LocalidadeCategoriaDAO.AtualizaCategorias();
                ItemCategoria cat = ItemCategoriaDAO.GetByID(id);
                if(cat != null)
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
        public ActionResult Edit(ItemCategoria cat)
        {
            // Início: Proteção de rota
            if (Session["usuario_nivel"] == null)
            {
                TempData["msg"] = "Necessário estar logado!";
                TempData["msg_type"] = "warning";
                return RedirectToAction("Index", "Home");
            }

            if (Convert.ToInt32(Session["usuario_nivel"]) > 1)
            {
                TempData["msg"] = "Você não tem autorização para acessar esta área!";
                TempData["msg_type"] = "danger";
                return RedirectToAction("Index", "Home");
            }
            // Fim: Proteção de Rota

            try
            {
                if (ItemCategoriaDAO.Update(cat))
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
            // Início: Proteção de rota
            if (Session["usuario_nivel"] == null)
            {
                TempData["msg"] = "Necessário estar logado!";
                TempData["msg_type"] = "warning";
                return RedirectToAction("Index", "Home");
            }

            if (Convert.ToInt32(Session["usuario_nivel"]) > 1)
            {
                TempData["msg"] = "Você não tem autorização para acessar esta área!";
                TempData["msg_type"] = "danger";
                return RedirectToAction("Index", "Home");
            }
            // Fim: Proteção de Rota

            try
            {
                if (ItemCategoriaDAO.Delete(id))
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
            // Início: Proteção de rota
            if (Session["usuario_nivel"] == null)
            {
                TempData["msg"] = "Necessário estar logado!";
                TempData["msg_type"] = "warning";
                return RedirectToAction("Index", "Home");
            }

            if (Convert.ToInt32(Session["usuario_nivel"]) > 1)
            {
                TempData["msg"] = "Você não tem autorização para acessar esta área!";
                TempData["msg_type"] = "danger";
                return RedirectToAction("Index", "Home");
            }
            // Fim: Proteção de Rota

            try
            {
                LocalidadeCategoriaDAO.AtualizaCategorias();
                return View(ItemCategoriaDAO.GetAll());
            }
            catch
            {
                return View();
            }
        }
    }
}
