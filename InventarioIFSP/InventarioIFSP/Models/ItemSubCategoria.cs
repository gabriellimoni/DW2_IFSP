using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioIFSP.Models
{
    public class ItemSubCategoria
    {
        private int ID { get; set; }
        private string Nome { get; set; }
        private string Descricao { get; set; }
        private ItemCategoria Categoria { get; set; }

        public ItemSubCategoria(int ID, string Nome, string Descricao, ItemCategoria Categoria)
        {
            this.ID = ID;
            this.Nome = Nome;
            this.Descricao = Descricao;
            this.Categoria = Categoria;
        }

        public ItemSubCategoria(string Nome, string Descricao, ItemCategoria Categoria)
        {
            this.Nome = Nome;
            this.Descricao = Descricao;
            this.Categoria = Categoria;
        }

        public ItemSubCategoria(int ID)
        {
            this.ID = ID;
        }



        public Boolean insert()
        {
            // comandos para inserir Sub-categoria de item


            return false;
        }

        public Boolean update()
        {
            // comandos para atualizar Sub-categoria de item


            return false;
        }

        public Boolean delete()
        {
            // comandos para deletar Sub-categoria de item


            return false;
        }
    }
}