using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventarioIFSP.Controllers;
using System.Web.Mvc;
using InventarioIFSP.Models;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDetailsView()
        {
            var controller = new ItemController();
            var result = controller.Details(2) as ViewResult;
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void TestDetailsViewData()
        {
            var controller = new ItemController();
            var result = controller.Details(2) as ViewResult;
            var item = (Item)result.ViewData.Model;
            Assert.AreEqual("cadeira", item.Patrimonio);
        }

        [TestMethod]
        public void TestDetailsRedirect()
        {
            var controller = new ItemController();
            var result = (RedirectToRouteResult)controller.Details(-1);
            Assert.AreEqual("Index", result.RouteValues["action"]);

        }

    }
}
