using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioIFSP.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int IdLocation { get; set; }
        public string Patrimony { get; set; }
        public int Category { get; set; }
        public int SubCategory { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
    }
}