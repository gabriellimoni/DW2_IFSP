using InventarioIFSP.ReportFramework;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioIFSP.Models
{
    public class Relatorio
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }
        public string SQL { get; set; }
        public int ColumnsNum { get; set; }
        public List<string> Fields { get; set; }

        public Relatorio(string SQL, int columnsNum, List<string> fields, string title, string header, string footer)
        {
            this.SQL = SQL;
            this.ColumnsNum = columnsNum;
            this.Title = title;
            this.Header = header;
            this.Footer = footer;
            this.Fields = fields;
        }
        

        internal ActionResult GenerateReport()
        {
            Report rep = new Report(this);

            return rep.ReportGenerator();
        }
    }
}