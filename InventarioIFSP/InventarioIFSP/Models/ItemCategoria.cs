using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Office = Microsoft.Office.Core;

namespace InventarioIFSP.Models
{
    public class ItemCategoria
    {
        private int ID { get; set; }
        private string Nome { get; set; }
        private string Descricao { get; set; }


        public ItemCategoria(int ID, string Nome, string Descricao)
        {
            this.ID = ID;
            this.Nome = Nome;
            this.Descricao = Descricao;
        }

        public ItemCategoria(int ID)
        {
            this.ID = ID;
        }

        public ItemCategoria(string Nome, string Descricao)
        {
            this.Nome = Nome;
            this.Descricao = Descricao;
        }



        public Boolean insert()
        {
            // comandos para inserir


            return false;
        }

        public Boolean update()
        {
            // comandos para atualizar


            return false;
        }

        public Boolean delete()
        {
            // comandos para deletar


            return false;
        }
    }
}