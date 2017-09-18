using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioIFSP.Models
{
    public class SalaCategoria
    {
        private int ID { get; set; }
        private string Nome { get; set; }
        private string Descricao { get; set; }


        public SalaCategoria(int ID, string Nome, string Descricao)
        {
            this.ID = ID;
            this.Nome = Nome;
            this.Descricao = Descricao;
        }


        public SalaCategoria(string Nome, string Descricao)
        {
            this.Nome = Nome;
            this.Descricao = Descricao;
        }

        public SalaCategoria(int ID)
        {
            this.ID = ID;
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