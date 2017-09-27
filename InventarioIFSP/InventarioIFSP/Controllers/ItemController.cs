using InventarioIFSP.Database;
using InventarioIFSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioIFSP.Controllers
{
    public class ItemController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            LocalidadeDAO.AtualizaLocalidades();
            ItemCategoriaDAO.AtualizaCategorias();
            ItemStatusDAO.AtualizaTiposStatus();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Item item)
        {
            try
            {
                try
                {
                    if (item.Observacao.Equals(string.Empty)) { }
                }
                catch (Exception)
                {
                    string str = "";
                    item.Observacao = str;
                }
                if(ItemDAO.Create(item) != null)
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
                LocalidadeDAO.AtualizaLocalidades();
                ItemCategoriaDAO.AtualizaCategorias();
                ItemStatusDAO.AtualizaTiposStatus();
                List<Item> lista = ItemDAO.GetAll();
                return View(lista);
            }
            catch
            {
                TempData["msg"] = "Erro ao buscar dados!";
                TempData["msg_type"] = "danger";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                LocalidadeDAO.AtualizaLocalidades();
                ItemCategoriaDAO.AtualizaCategorias();
                ItemStatusDAO.AtualizaTiposStatus();
                Item item = ItemDAO.GetByID(id);
                if (item != null)
                {
                    return View(item);
                }
            }
            catch (Exception)
            {
            }
            TempData["msg"] = "Erro ao buscar dados!";
            TempData["msg_type"] = "danger";
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Item item)
        {
            try
            {
                try
                {
                    if (item.Observacao.Equals(string.Empty)) { }
                }
                catch (Exception)
                {
                    string str = "";
                    item.Observacao = str;
                }
                if (ItemDAO.Update(item))
                {
                    TempData["msg"] = "Atualizado com sucesso!";
                    TempData["msg_type"] = "success";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
            }
            TempData["msg"] = "Erro ao buscar dados!";
            TempData["msg_type"] = "danger";
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                if (ItemDAO.Delete(id))
                {
                    TempData["msg"] = "Atualizado com sucesso!";
                    TempData["msg_type"] = "success";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
            }
            TempData["msg"] = "Erro ao excluir!";
            TempData["msg_type"] = "danger";
            return RedirectToAction("Index");
        }
    }
}
