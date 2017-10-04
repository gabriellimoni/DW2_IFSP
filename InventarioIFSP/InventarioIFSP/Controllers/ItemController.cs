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

            LocalidadeDAO.AtualizaLocalidades();
            ItemCategoriaDAO.AtualizaCategorias();
            ItemStatusDAO.AtualizaTiposStatus();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Item item)
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

        //GET: Item/Details
        public ActionResult Details(int id)
        {
            if (id < 1)
                return RedirectToAction("Index");
            var item = new Item();
            item.ID = id;
            item.Patrimonio = "cadeira";
            return View("Details", item);
        }

    }
}
