using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventarioIFSP.Database;
using InventarioIFSP.Models;

namespace InventarioIFSP.Tests
{
    [TestClass]
    public class TestItemCategoria
    {
        private ItemCategoria cat_up = new ItemCategoria
        {
            Nome = "Categoria Teste Update delete",
            Descricao = "Testando categora de item update delete"
        };

        private ItemCategoria cat = new ItemCategoria
        {
            Nome = "Categoria Teste",
            Descricao = "Testando categora de item"
        };

        [TestInitialize]
        public void init()
        {
            cat_up.ID = (int)ItemCategoriaDAO.Create(cat_up);
        }

        [TestCleanup]
        public void clean()
        {
            ItemCategoriaDAO.Delete(cat.ID);
            ItemCategoriaDAO.Delete(cat_up.ID);
        }


        [TestMethod]
        public void createTest()
        {
            Assert.IsNotNull(cat.ID);
        }

        [TestMethod]
        public void updateTest()
        {
            cat_up.Nome = "Teste ___";
            Assert.IsTrue(ItemCategoriaDAO.Update(cat_up));
        }

        [TestMethod]
        public void deleteTest()
        {
            Assert.IsTrue(ItemCategoriaDAO.Delete(cat_up.ID));
        }

    }
}