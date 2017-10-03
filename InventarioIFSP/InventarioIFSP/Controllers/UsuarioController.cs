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
            if (Session["usuario_nivel"] != null) // Proteção de rota
            {
                if (Convert.ToInt32(Session["usuario_nivel"]) == 0)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    TempData["msg"] = "Você não tem autorização para acessar esta área!";
                    TempData["msg_type"] = "danger";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["msg"] = "Necessário estar logado!";
                TempData["msg_type"] = "warning";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            string valor = Request.Params["valor"];
            string senha = Request.Params["senha"];
            Usuario u = UsuarioDAO.Login(usuario.Email, usuario.Senha);
            if (u != null)
            {
                Session["usuario_id"] = u.ID;
                Session["usuario_nome"] = u.Nome;
                Session["usuario_email"] = u.Email;
                Session["usuario_nivel"] = u.Nivel;
                return RedirectToAction("Index", "Inventario");
            }

            TempData["msg"] = "Dados não conferem!";
            TempData["msg_type"] = "danger";
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            try
            {
                Session["usuario_id"] = null;
                Session["usuario_nome"] = null;
                Session["usuario_email"] = null;
                Session["usuario_nivel"] = null;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["usuario_nivel"] != null) // Proteção de rota
            {
                if (Convert.ToInt32(Session["usuario_nivel"]) == 0)
                {
                    return View();
                }
                else
                {
                    TempData["msg"] = "Você não tem autorização para acessar esta área!";
                    TempData["msg_type"] = "danger";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["msg"] = "Necessário estar logado!";
            TempData["msg_type"] = "warning";
            return RedirectToAction("Index", "Home");
        }
    

        [HttpPost]
        public ActionResult Create(Usuario user)
        {
            if (Session["usuario_nivel"] != null) // Proteção de rota
            {
                if (Convert.ToInt32(Session["usuario_nivel"]) == 0)
                {
                    if (UsuarioDAO.Create(user) != null)
                    {
                        TempData["msg_type"] = "success";
                        TempData["msg"] = "Criado com sucesso!";
                        return RedirectToAction("List");
                    }

                    TempData["msg_type"] = "danger";
                    TempData["msg"] = "Erro na criação!";
                    return View();
                }
                else
                {
                    TempData["msg"] = "Você não tem autorização para acessar esta área!";
                    TempData["msg_type"] = "danger";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["msg"] = "Necessário estar logado!";
            TempData["msg_type"] = "warning";
            return RedirectToAction("Index", "Home");


            
        }

        [HttpGet]
        public ActionResult List()
        {
            if (Session["usuario_nivel"] != null) // Proteção de rota
            {
                if (Convert.ToInt32(Session["usuario_nivel"]) == 0)
                {
                    List<Usuario> users = UsuarioDAO.GetAll();
                    if (users == null)
                    {
                        users = new List<Usuario>();
                        TempData["msg"] = "Erro ao buscar usuários";
                        TempData["msg_type"] = "danger";
                        return RedirectToAction("Index", "Home");
                    }
                    return View(users);
                }
                else
                {
                    TempData["msg"] = "Você não tem autorização para acessar esta área!";
                    TempData["msg_type"] = "danger";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["msg"] = "Necessário estar logado!";
            TempData["msg_type"] = "warning";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["usuario_nivel"] != null) // Proteção de rota
            {
                if (Convert.ToInt32(Session["usuario_nivel"]) == 0)
                {
                    Usuario u = UsuarioDAO.GetByID(id);
                    if (u != null)
                    {
                        return View(u);
                    }
                    TempData["msg"] = "Erro ao buscar usuário";
                    TempData["msg_type"] = "danger";
                    return RedirectToAction("List", "Usuario");
                }
                else
                {
                    TempData["msg"] = "Você não tem autorização para acessar esta área!";
                    TempData["msg_type"] = "danger";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["msg"] = "Necessário estar logado!";
            TempData["msg_type"] = "warning";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Edit(Usuario usuario)
        {
            if (Session["usuario_nivel"] != null) // Proteção de rota
            {
                if (Convert.ToInt32(Session["usuario_nivel"]) == 0)
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
                else
                {
                    TempData["msg"] = "Você não tem autorização para acessar esta área!";
                    TempData["msg_type"] = "danger";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["msg"] = "Necessário estar logado!";
            TempData["msg_type"] = "warning";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["usuario_nivel"] != null) // Proteção de rota
            {
                if (Convert.ToInt32(Session["usuario_nivel"]) == 0)
                {
                    if (UsuarioDAO.Delete(id))
                    {
                        TempData["msg"] = "Excluído com sucesso!";
                        TempData["msg_type"] = "success";
                        return RedirectToAction("List");
                    }
                    TempData["msg_type"] = "danger";
                    TempData["msg"] = "Erro ao excluir!";
                    return View();
                }
                else
                {
                    TempData["msg"] = "Você não tem autorização para acessar esta área!";
                    TempData["msg_type"] = "danger";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["msg"] = "Necessário estar logado!";
            TempData["msg_type"] = "warning";
            return RedirectToAction("Index", "Home");
        }

        //GET: Item/Details
        public ActionResult Details(int id)
        {
            if (id < 1)
                return RedirectToAction("Index");
            var usuario = new Usuario();
            usuario.ID = id;
            usuario.Nome = "Rosana";
            return View("Details", usuario);
        }
    }
}