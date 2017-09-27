using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inventario_IFSPPRC.Models;
using InventarioIFSP.Database;

namespace InventarioIFSP.Tests
{
    [TestClass]
    public class TestUsuario
    {
        private Usuario usuario = new Usuario
        {
            Nome = "Teste User",
            Prontuario = "1235658",
            Email = "teste@bol.com.br",
            Senha = "1928*72829"
        };

        private Usuario usuario_create;

        private int num;

        [TestInitialize]
        public void init()
        {
            usuario.ID = (int)UsuarioDAO.Create(usuario);
        }

        [TestCleanup]
        public void clean()
        {
            UsuarioDAO.Delete(usuario.ID);
            UsuarioDAO.Delete(num);
        }

        [TestMethod]
        public void createTest()
        {
            usuario_create = new Usuario
            {
                Nome = "Teste User Create",
                Prontuario = "1235651",
                Email = "teste@bol2.com.br",
                Senha = "1928*72827"
            };

            Assert.IsNotNull(num = (int)UsuarioDAO.Create(usuario_create));
        }

        [TestMethod]
        public void updateTest()
        {
            usuario.Nome = "Teste User Update";
            Assert.IsTrue(UsuarioDAO.Update(usuario));
        }

        [TestMethod]
        public void updateSelfTest()
        {
            usuario.Nome = "Teste User Update_SELF";
            Assert.IsTrue(UsuarioDAO.UpdateSelf(usuario));
        }
        


    }
}