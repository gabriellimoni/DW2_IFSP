using InventarioIFSP.Database;
using InventarioIFSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioIFSP.Controllers
{
    public class InventarioController : Controller
    {
        public ActionResult Index()
        {
            // Início: Proteção de rota
            if (Session["usuario_nivel"] == null)
            {
                TempData["msg"] = "Necessário estar logado!";
                TempData["msg_type"] = "warning";
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("SelecionaLocalidade");
        }

        public ActionResult Inventario()
        {
            // Início: Proteção de rota
            if (Session["usuario_nivel"] == null)
            {
                TempData["msg"] = "Necessário estar logado!";
                TempData["msg_type"] = "warning";
                return RedirectToAction("Index", "Home");
            }

            if (Convert.ToInt32(Session["usuario_nivel"]) > 0)
            {
                TempData["msg"] = "Você não tem autorização para acessar esta área!";
                TempData["msg_type"] = "danger";
                return RedirectToAction("Index", "Home");
            }
            // Fim: Proteção de Rota

            List<Inventario> list;
            if (!InventarioDAO.CheckStatus())
            {
                string semestre = "/1";
                if (DateTime.Now.Month > 6)
                    semestre = "/2";
                TempData["semestre_ano"] = DateTime.Now.Year.ToString() + semestre;
            }
             list = InventarioDAO.GetAll();
             if(list == null)
                list = new List<Inventario>();
            return View(list);
        }

        public ActionResult Create()
        {
            int ano = DateTime.Now.Year;
            int semestre = DateTime.Now.Month;
            if (!InventarioDAO.CheckStatus())
            {
                if (InventarioDAO.Create())
                {
                    TempData["msg"] = "Consolidado com sucesso!";
                    TempData["msg_type"] = "success";
                    return RedirectToAction("Inventario", "Inventario");
                }
                TempData["msg"] = "Ocorreu um erro!";
                TempData["msg_type"] = "danger";
                return RedirectToAction("Inventario", "Inventario");

            }
            else
            {
                TempData["msg"] = "Inventário já consolidado!";
                TempData["msg_type"] = "warning";
                return RedirectToAction("Inventario", "Inventario");
            }
        }

        public ActionResult SelecionaLocalidade(string nomes)
        {
            // Início: Proteção de rota
            if (Session["usuario_nivel"] == null)
            {
                TempData["msg"] = "Necessário estar logado!";
                TempData["msg_type"] = "warning";
                return RedirectToAction("Index", "Home");
            }
            // Fim: Proteção de Rota

            List<SelectListItem> lista = LocalidadeDAO.GetSelectList();
            TempData["localidades"] = lista;

            return View();
        }

        public ActionResult Update(Localidade local)
        {
            // Início: Proteção de rota
            if (Session["usuario_nivel"] == null)
            {
                TempData["msg"] = "Necessário estar logado!";
                TempData["msg_type"] = "warning";
                return RedirectToAction("Index", "Home");
            }
            // Fim: Proteção de Rota

            try
            {
                ItemStatusDAO.AtualizaTiposStatus();

                TempData["localidade"] = LocalidadeDAO.GetByID(local.ID);
                TempData["itens"] = ItemDAO.GetByLocalidade(local.ID);
                TempData["todos_itens"] = ItemDAO.GetAll();
                TempData["lista_status"] = ItemStatusDAO.lista_status;
                
                return View();
            }
            catch (Exception)
            {
                TempData["msg"] = "Ocorreu um erro!";
                TempData["msg_type"] = "danger";
                return RedirectToAction("Index", "Home");
            }
           
        }

        public ActionResult UpdateViaJS()
        {
            // Início: Proteção de rota
            if (Session["usuario_nivel"] == null)
            {
                TempData["msg"] = "Necessário estar logado!";
                TempData["msg_type"] = "warning";
                return RedirectToAction("Index", "Home");
            }
            // Fim: Proteção de Rota

            Localidade local = LocalidadeDAO.GetByID(Convert.ToInt32(Request["localidade_id"]));

            try
            {
                ItemStatusDAO.AtualizaTiposStatus();

                TempData["localidade"] = LocalidadeDAO.GetByID(local.ID);
                TempData["itens"] = ItemDAO.GetByLocalidade(local.ID);
                TempData["todos_itens"] = ItemDAO.GetAll();
                TempData["lista_status"] = ItemStatusDAO.lista_status;

                return View("Update");
            }
            catch (Exception)
            {
                TempData["msg"] = "Ocorreu um erro!";
                TempData["msg_type"] = "danger";
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult Delete()
        {
            // Início: Proteção de rota
            if (Session["usuario_nivel"] == null)
            {
                TempData["msg"] = "Necessário estar logado!";
                TempData["msg_type"] = "warning";
                return RedirectToAction("Index", "Home");
            }

            if (Convert.ToInt32(Session["usuario_nivel"]) > 0)
            {
                TempData["msg"] = "Você não tem autorização para acessar esta área!";
                TempData["msg_type"] = "danger";
                return RedirectToAction("Index", "Home");
            }
            // Fim: Proteção de Rota

            int id = Convert.ToInt32(Request["inventario_id"]);
            if (InventarioDAO.Delete(id))
            {
                TempData["msg"] = "Excluído com sucesso!";
                TempData["msg_type"] = "success";
                return RedirectToAction("Inventario", "Inventario");
            }
            TempData["msg"] = "Ocorreu um erro!";
            TempData["msg_type"] = "danger";
            return RedirectToAction("Inventario", "Inventario");
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

            if (Convert.ToInt32(Session["usuario_nivel"]) > 0)
            {
                TempData["msg"] = "Você não tem autorização para acessar esta área!";
                TempData["msg_type"] = "danger";
                return RedirectToAction("Index", "Home");
            }
            // Fim: Proteção de Rota

            TempData["itens"] = InventarioDAO.GetItens(id);
            return View(InventarioDAO.GetByID(id));
        }

    }
}