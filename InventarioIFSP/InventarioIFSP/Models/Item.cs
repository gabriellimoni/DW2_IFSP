using InventarioIFSP.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioIFSP.Models
{
    public class Item
    {
        public int ID { get; set; }

        [Display(Name = "Patrimônio")]
        public string Patrimonio { get; set; }
        public ItemCategoria Categoria { get; set; }
        public Localidade Localidade { get; set; }
        public ItemStatus Status{ get; set; } // Cadastro BD

        [Display(Name = "Observação")]
        public string Observacao { get; set; } = "";
        public IEnumerable<SelectListItem> Tipos_Status { get; set; }
        public IEnumerable<SelectListItem> Localidades { get; set; }
        public IEnumerable<SelectListItem> Categorias { get; set; }

    }
}