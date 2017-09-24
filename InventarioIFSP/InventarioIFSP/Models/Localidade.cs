using InventarioIFSP.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioIFSP.Models
{
    public class Localidade
    {
        public int ID { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Bloco { get; set; }
        public LocalidadeCategoria Categoria { get; set; }
        public static IEnumerable<SelectListItem> Categorias { get; set; }// = LocalidadeCategoriaDAO.GetTipos();
    }

}