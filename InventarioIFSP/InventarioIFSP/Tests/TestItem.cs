using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventarioIFSP.Database;
using InventarioIFSP.Models;

namespace InventarioIFSP.Tests
{
    [TestClass]
    public class TestItem
    {
        private ItemCategoria cat = new ItemCategoria
        {
            Nome = "Categoria Teste",
            Descricao = "Testando categora de item"
        };
        private ItemStatus st = new ItemStatus
        {
            Nome = "st1",
            Descricao = "Nenhuma descricao"
        };
        private Localidade loc = new Localidade
        {
            Nome = "21",
            Bloco = "B"
        };
        private LocalidadeCategoria locCat = new LocalidadeCategoria
        {
            Nome = "Sala",
            Descricao = "Nenhuma descricao"
        };
        private Item item_;

        private Item item;

        [TestInitialize]
        public void init()
        {
            cat.ID = (int)ItemCategoriaDAO.Create(cat);
            st.Id = (int)ItemStatusDAO.Create(st);
            locCat.ID = (int)LocalidadeCategoriaDAO.Create(locCat);
            loc.Categoria = locCat;
            loc.ID = (int)LocalidadeDAO.Create(loc);
            


            item = new Item
            {
                Patrimonio = "123456854",
                Categoria = cat,
                Localidade = loc,
                Status = st,
                Observacao = "nenhuma observação"
            };
            item.ID = (int)ItemDAO.Create(item);
        }

        [TestCleanup]
        public void clean()
        {
            ItemDAO.Delete(item.ID);
            try
            {
                ItemDAO.Delete(item_.ID);
            } catch (Exception e) { }
            

            ItemCategoriaDAO.Delete(cat.ID);
            ItemStatusDAO.Delete(st.Id);
            LocalidadeDAO.Delete(loc.ID);
            LocalidadeCategoriaDAO.Delete(locCat.ID);

            
        }


        [TestMethod] 
        public void createTest()
        {
            item_ = new Item
            {
                Patrimonio = "123asd456854",
                Categoria = cat,
                Localidade = loc,
                Status = st,
                Observacao = "nenhuma observação"
            };
            item_.ID = (int)ItemDAO.Create(item_);
            Assert.IsNotNull(item_.ID);
        }

        [TestMethod]
        public void updateTest()
        {
            item.Patrimonio = "6546875421";
            Assert.IsTrue(ItemDAO.Update(item));
        }

        [TestMethod]
        public void deleteTest()
        {
            Assert.IsTrue(ItemDAO.Delete(item.ID));
        }
    }
}