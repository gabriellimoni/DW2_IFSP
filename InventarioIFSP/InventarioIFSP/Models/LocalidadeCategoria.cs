using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventarioIFSP.Models
{
    public class LocalidadeCategoria
    {
        public int ID { get; set; }

        [Required]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}