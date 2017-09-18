using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioIFSP.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        public int IdCategory { get; set; }
        public string Name { get; set; }
    }
}