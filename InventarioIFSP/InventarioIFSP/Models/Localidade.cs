using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioIFSP.Models
{
    public class Localidade
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public LocalidadeCategoria Categoria { get; set; }
        public string Bloco { get; set; }


        public Localidade(int ID, string Nome, LocalidadeCategoria Categoria, string Bloco)
        {
            this.ID = ID;
            this.Nome = Nome;
            this.Categoria = Categoria;
            this.Bloco = Bloco;
        }

        public Localidade(string Nome, LocalidadeCategoria Categoria, string Bloco)
        {
            this.Nome = Nome;
            this.Categoria = Categoria;
            this.Bloco = Bloco;
        }

        public Localidade(int ID)
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