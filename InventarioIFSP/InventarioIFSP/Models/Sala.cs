using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioIFSP.Models
{
    public class Sala
    {
        private int ID { get; set; }
        private string Nome { get; set; }
        private SalaCategoria Categoria { get; set; }
        private string Bloco { get; set; }


        public Sala(int ID, string Nome, SalaCategoria Categoria, string Bloco)
        {
            this.ID = ID;
            this.Nome = Nome;
            this.Categoria = Categoria;
            this.Bloco = Bloco;
        }

        public Sala(string Nome, SalaCategoria Categoria, string Bloco)
        {
            this.Nome = Nome;
            this.Categoria = Categoria;
            this.Bloco = Bloco;
        }

        public Sala(int ID)
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



        public Boolean atribuirItem(Item Item)
        {
            // comandos para atribuir algum item 

            return false;
        }

    }
}