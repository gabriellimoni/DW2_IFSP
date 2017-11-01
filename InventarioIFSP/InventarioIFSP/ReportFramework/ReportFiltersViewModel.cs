using InventarioIFSP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventarioIFSP.ReportFramework
{
    public class ReportFiltersViewModel
    {
        [Display(Name = "Localidade")]
        public Localidade LOC { get; set; }
        [Display(Name = "Status")]
        public ItemStatus STA { get; set; }
        [Display(Name = "Categoria de Localidade")]
        public LocalidadeCategoria LOCCAT { get; set; }
        [Display(Name = "Categoria do Item")]
        public ItemCategoria ITEMCAT { get; set; }

    }
}