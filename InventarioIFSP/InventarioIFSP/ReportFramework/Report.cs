using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Data;
using System.Web.Mvc;
using InventarioIFSP.Models;

namespace InventarioIFSP.ReportFramework   
{
    public class Report : Controller      // retorna uma página HTML formatada com os dados da consulta
    {
        private static NpgsqlConnection dbConn;
        public Relatorio REL;

        // Cria objeto de conexão, se já existir abre a conexão
        private static void OpenConn()
        {
            if (dbConn == null)
            {
                dbConn = Database.Database.Conexao;
            }
            else
            {
                if (dbConn.State != ConnectionState.Open)
                    dbConn.Open();
            }
        }



        public Report(Relatorio rep)
        {
            this.REL = rep;
        }



        internal ViewResult ReportGenerator()
        {
            // executar query
            DataTable table = new DataTable();
            List<string> lista = new List<string>();
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = REL.SQL;
                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {// monta lista com conteúdo formatado
                        lista.Add(
                            generateRow(dr, REL.ColumnsNum)
                            );
                    }
                }
                dbConn.Close();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/ItemCategoriaDAO/GetAll:: " + e);
                dbConn.Close();
                return null;
            }

            // cria cabeçalho do content
            string columnsHTML = generateHeader(REL.Fields);

            // criar ReportViewModel
            ReportViewModel repView = new ReportViewModel
            {
                Header = REL.Header,
                Title = REL.Title,
                Footer = REL.Footer,
                Columns = columnsHTML,
                Contents = lista
            };


            // retornar a ReportView com o ReportViewModel de parametro
            return View("~/Views/ReportView.cshtml", repView);
        }


        private string generateRow(DataRow dr, int num)  // 6 fields max
        {
            int col = 12 / num;

            string row = "<div class='row row-striped'>";
            for (int i = 0; i < num; i++)
            {
                // trata se for numero 5
                if (num == 5)
                    if (i == 0)
                        col += 1;
                    else if (i == 1)
                        col -= 1;

                row += "<div class='col col-lg-"+col+"'>" +
                           dr[i].ToString() +
                       "</div>";
            }
            row += "</div>";

            return row;
        }

        private string generateHeader(List<string> fields)
        {
            int num = fields.Count;

            int col = 12 / REL.ColumnsNum;

            string row = "<div class='row columns row-striped'>";
            for (int i = 0; i < REL.ColumnsNum; i++)
            {
                // trata se for numero 5
                if (num == 5)
                    if (i == 0)
                        col += 1;
                    else if (i == 1)
                        col -= 1;

                row += "<div class='col col-lg-" + col + "'>" +
                           "<h3>"+ fields[i] + "</h3>"+
                        "</div>";
            }
            row += "</div>";

            return row;
        }

    }

}