using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventarioIFSP.Controllers;
using Inventario_IFSPPRC.Models;
using System.Web.Mvc;

namespace TestProjectUsuario
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDetailsView()
        {
            var controller = new UsuarioController();
            var result = controller.Details(2) as ViewResult;
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void TestDetailsViewData()
        {
            var controller = new UsuarioController();
            var result = controller.Details(2) as ViewResult;
            var usuario = (Usuario)result.ViewData.Model;
            Assert.AreEqual("Rosana", usuario.Nome);
        }

        [TestMethod]
        public void TestDetailsRedirect()
        {
            var controller = new UsuarioController();
            var result = (RedirectToRouteResult)controller.Details(-1);
            Assert.AreEqual("Index", result.RouteValues["action"]);

        }

    }
}
