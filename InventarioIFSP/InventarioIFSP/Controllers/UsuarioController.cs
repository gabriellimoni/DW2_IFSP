using Inventario_IFSPPRC.Models;
using InventarioIFSP.Database;
using InventarioIFSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioIFSP.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Usuario user)
        {
            var result = UsuarioDAO.Create(user);
            if (result != null)
            {
                TempData["msg_type"] = "success";
                TempData["msg"] = "Criado com sucesso!";
                return RedirectToAction("List");
            }
  
            TempData["msg_type"] = "danger";
            TempData["msg"] = "Erro na criação!";
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            //UsuarioDAO.Login();
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            List<Usuario> users = UsuarioDAO.GetAll();
            if (users == null)
            {
                users = new List<Usuario>();
                TempData["msg"] = "Erro ao buscar usuários";
                TempData["msg_type"] = "danger";
            }
            return View(users);
        }


        

        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Lista de itens do dropdown

            Usuario u = UsuarioDAO.GetByID(id);
            return View(u);
        }

        [HttpPost]
        public ActionResult Edit(Usuario usuario)
        {
            if (UsuarioDAO.Update(usuario))
            {
                TempData["msg_type"] = "success";
                TempData["msg"] = "Alterado com sucesso!";
                return RedirectToAction("List");
            }
            TempData["msg_type"] = "danger";
            TempData["msg"] = "Erro ao alterar. Verifique os campos!";
            return View(usuario);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            //TODO: Verifica se está autorizado a excluir

            if(UsuarioDAO.Delete(id))
            {
                TempData["msg"] = "Excluído com sucesso!";
                TempData["msg_type"] = "success";
                return RedirectToAction("List");
            }
            TempData["msg_type"] = "danger";
            TempData["msg"] = "Erro ao excluir!";
            return View();
        }
    }
}