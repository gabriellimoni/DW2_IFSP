using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioIFSP.Models
{
    public class Location
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Block { get; set; }
        public string Description { get; set; }
        public int capacity { get; set; }
        public List<Item> Items { get; set; }
    }
}