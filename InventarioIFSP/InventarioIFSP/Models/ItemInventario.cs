using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioIFSP.Models
{
    public class ItemInventario
    {
        public int Id { get; set; }
        public int Inventario { get; set; }
        public string Patrimonio { get; set; }
        public string Categoria { get; set; }
        public string Localidade { get; set; }
        public string Bloco { get; set; }
        public string Status { get; set; }
    }
}