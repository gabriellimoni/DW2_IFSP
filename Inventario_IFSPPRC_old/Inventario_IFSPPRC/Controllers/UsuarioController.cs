using Inventario_IFSPPRC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventario_IFSPPRC.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listagem()
        {
            var users = from u in ListaUsuario
                        orderby u.Nome
                        select u;
            return View(users);
        }


        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(Usuario user)
        {
            try
            {
                // TODO: Add insert logic here
                user.ID++;
                ListaUsuario.Add(user);

                return RedirectToAction("Listagem");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            Usuario usu = ListaUsuario.Single(u => u.ID == id);
            return View(usu);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var user = ListaUsuario.Single(u => u.ID == id);
                if(TryUpdateModel(user))
                    return RedirectToAction("Listagem");
                return View(user);
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Listagem");
            }
            catch
            {
                return View();
            }
        }


        public static List<Usuario> ListaUsuario = new List<Usuario>
        {
            new Usuario
            {
                ID = 1,
                Nome = "Rosana",
                CPF = "453.956.488.95",
                Matricula = "PC15502020",
                Telefone = "(19)1928198239",
                Funcoes = new List<int>{0,1,2,3,4,5,6}
            },
            new Usuario
            {
                ID = 2,
                Nome = "Gabriel",
                CPF = "453.956.488.95",
                Matricula = "PC15502020",
                Telefone = "(19)1928198239",
                Funcoes = new List<int>{0,1,2,3,4,5,6}
            },
            new Usuario
            {
                ID = 3,
                Nome = "Eraldo",
                CPF = "453.956.488.95",
                Matricula = "PC15502020",
                Telefone = "(19)1928198239",
                Funcoes = new List<int>{0,1,2,3,4,5,6}
            },
            new Usuario
            {
                ID = 4,
                Nome = "Tatiane",
                CPF = "453.956.488.95",
                Matricula = "PC15502020",
                Telefone = "(19)1928198239",
                Funcoes = new List<int>{0,1,2,3,4,5,6}
            }
        };

    }
}
