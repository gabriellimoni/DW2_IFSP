using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioIFSP.Models
{
    public class LocalidadeCategoria
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public LocalidadeCategoria(int ID, string Nome, string Descricao)
        {
            this.ID = ID;
            this.Nome = Nome;
            this.Descricao = Descricao;
        }

        public LocalidadeCategoria(string Nome, string Descricao)
        {
            this.Nome = Nome;
            this.Descricao = Descricao;
        }

        public LocalidadeCategoria(int ID)
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