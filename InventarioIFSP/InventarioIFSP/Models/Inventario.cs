using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventarioIFSP.Models
{
    public class Inventario
    {
        public int Id { get; set; }
        public int Semestre  { get; set; }
        public int Ano { get; set; }
        [Display(Name = "Consolidado?")]
        public string Consolidado { get; set; } // Sim/Não - Se estiver consolidado, não pode ser alterado
    }
}