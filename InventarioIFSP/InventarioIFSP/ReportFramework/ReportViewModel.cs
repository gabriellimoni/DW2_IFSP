using InventarioIFSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioIFSP.ReportFramework
{
    public class ReportViewModel
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }
        public string Columns { get; set; }
        public List<string> Contents { get; set; }
    }
}