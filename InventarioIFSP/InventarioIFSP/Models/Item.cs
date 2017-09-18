using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioIFSP.Models
{
    public class Item
    {
        public static List<Item> listarTodosItens()
        {
            return null; // comando para retornar todos os itens
        }
        // analisar quais filtros serão necessários e.g: Sub_Categoria, Categoria, Sala_Atual...

        private int ID { get; set; }
        private string Patrimonio_Numero { get; set; }
        private ItemSubCategoria Sub_Categoria { get; set; }
        private Sala Sala_Atual { get; set; }
        private Sala Sala_Anterior { get; set; }
        private int Status { get; set; } // padronizar numeração do status ex: 0-alocado, 1-desalocado, 2-desmanche...


        public Item(int ID, string Patrimonio_Numero, ItemSubCategoria Sub_Categoria, Sala Sala_Atual,
                        Sala Sala_Anterior, int Status)
        {
            this.ID = ID;
            this.Patrimonio_Numero = Patrimonio_Numero;
            this.Sub_Categoria = Sub_Categoria;
            this.Sala_Atual = Sala_Atual;
            this.Sala_Anterior = Sala_Anterior;
            this.Status = Status;
        }

        public Item(string Patrimonio_Numero, ItemSubCategoria Sub_Categoria, Sala Sala_Atual,
                        Sala Sala_Anterior, int Status)
        {
            this.Patrimonio_Numero = Patrimonio_Numero;
            this.Sub_Categoria = Sub_Categoria;
            this.Sala_Atual = Sala_Atual;
            this.Sala_Anterior = Sala_Anterior;
            this.Status = Status;
        }

        public Item(int ID)
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





        public Boolean atribuirSalaAtual(Sala Sala)
        {
            // comandos para atribuir item para determinada sala


            return false;
        }
        
        public Boolean removerSalaAtual() // desalocar
        {
            // comandos para desalocar o item

            return false;
        }
    }
}