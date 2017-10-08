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
                TempData["lista_status"] = ItemStatusDAO.lista_status;
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

        // Esta action recebe os dados do ajax para atualizar os dados do inventario
        [HttpPost]
        public ActionResult AtualizaStatus()
        {
            int id = Convert.ToInt32(Request["id"]);
            int status = Convert.ToInt32(Request["status"]);
            Item item = ItemDAO.GetByID(id);
            item.Status.Id = status;
            if (ItemDAO.Update(item))
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        // Esta action recebe os dados do ajax para atualizar os dados do inventario
        [HttpPost]
        public ActionResult AtualizaObs()
        {
            int id = Convert.ToInt32(Request["id"]);
            string observacao = Request["observacao"];
            Item item = ItemDAO.GetByID(id);
            item.Observacao = observacao;
            if (ItemDAO.Update(item))
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
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
                Item item = ItemDAO.GetByID(id);
                return View("Details", item);
            }
            catch (Exception)
            {
                TempData["msg"] = "Erro ao buscar dados!";
                TempData["msg_type"] = "danger";
                return View();
            }
            
        }

        public ActionResult AtualizaLocalidade()
        {
            int id = Convert.ToInt32(Request["id"]);
            int localidade = Convert.ToInt32(Request["localidade"]);
            Item item = ItemDAO.GetByID(id);
            item.Localidade.ID = localidade;
            if (ItemDAO.Update(item))
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }


    }
}
