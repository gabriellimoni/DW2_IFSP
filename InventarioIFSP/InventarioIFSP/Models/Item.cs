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

        public int ID { get; set; }
        public string Patrimonio { get; set; }
        public ItemCategoria Categoria { get; set; }
        public ItemSubCategoria Subcategoria { get; set; }
        public Localidade Localidade { get; set; }
        public ItemStatus Status{ get; set; } // padronizar numeração do status ex: 0-alocado, 1-desalocado, 2-desmanche...

        public Item(int ID, string Patrimonio_Numero, ItemCategoria Categoria, ItemSubCategoria Sub_Categoria, Localidade Localidade, ItemStatus Status)
        {
            this.ID = ID;
            this.Patrimonio = Patrimonio_Numero;
            this.Categoria = Categoria;
            this.Subcategoria = Sub_Categoria;
            this.Localidade = Localidade;
            this.Status = Status;
        }
    }
}