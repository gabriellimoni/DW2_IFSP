using InventarioIFSP.Database;
using InventarioIFSP.ReportFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioIFSP.Models
{
    public class RelatorioController : Controller
    {
        // GET: Relatorio
        public ActionResult Index()
        {
            LocalidadeDAO.AtualizaLocalidades();
            ItemCategoriaDAO.AtualizaCategorias();
            ItemStatusDAO.AtualizaTiposStatus();
            LocalidadeCategoriaDAO.AtualizaCategorias();
            return View();
        }


        public ActionResult GenerateReportTest(ReportFiltersViewModel rep)
        {
            string SQL = "SELECT " +
                                "l.nome AS Localidade, " +
                                "i.patrimonio AS Patrimonio, " +
                                "c.nome AS Categoria, " +
                                "s.nome AS Status " +
                            "FROM " +
                                "localidade l INNER JOIN item i on l.id = i.localidade " +
                                "INNER JOIN item_categoria c ON c.id = i.categoria " +
                                "INNER JOIN item_status s ON s.id = i.status";


            string WHERE= " ";
            bool GotBeforeFilter = false;

            if (rep.LOC.ID > 0 || rep.ITEMCAT.ID > 0 || rep.STA.Id > 0)
            {
                WHERE += " WHERE ";

                if (rep.LOC.ID > 0)
                {
                    WHERE += " l.id = " + rep.LOC.ID;
                    GotBeforeFilter = true;
                }

                if (rep.ITEMCAT.ID > 0)
                {
                    if (GotBeforeFilter)
                        WHERE += " AND ";

                    WHERE += " c.id = " + rep.ITEMCAT.ID;

                    GotBeforeFilter = true;
                }

                if (rep.STA.Id > 0)
                {
                    if (GotBeforeFilter)
                        WHERE += " AND ";

                    WHERE += " s.id = " + rep.STA.Id;
                }
            }


            string ORDER= " ORDER BY 1,3,4"; // implementar opção para o usuário escolher via DropDownList

            SQL += WHERE + ORDER;

            List<string> fields = new List<string>();
            fields.Add("Localidade");
            fields.Add("Patrimonio");
            fields.Add("Categoria");
            fields.Add("Status");

            Relatorio rel = new Relatorio(
                    SQL, 
                    4,
                    fields,
                    "Itens", 
                    "Listagem de itens", 
                    ""
                );

            return rel.GenerateReport();
        }
    }
}