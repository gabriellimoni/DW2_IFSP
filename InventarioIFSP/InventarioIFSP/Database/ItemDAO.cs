using InventarioIFSP.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioIFSP.Database
{
    public class ItemDAO
    {
        private NpgsqlConnection dbConn;
        public ItemDAO()
        {
            dbConn = Database.Conexao;
        }

        public bool Create(Item item)
        {
            try
            {
                string sql = String.Format("INSERT INTO item(Patrimonio, Categoria, Subcategoria, Status) values('{0}',{1},{2},{3})",
                    item.Patrimonio, item.Categoria, item.Subcategoria, item.Status);

                new NpgsqlCommand(sql, dbConn).ExecuteNonQuery();
                dbConn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("INVENTARIO/itemDAO/Create:: " + e);
                dbConn.Close();
                return false;
            }
        }

        public bool Update(Item item)
        {
            try
            {
                NpgsqlConnection dbConn = Database.Conexao;
                string sql = String.Format("UPDATE item(Patrimonio, Categoria, Subcategoria, Localidade, Status) values('{0}',{1},{2},{3},{4}) WHERE id = {5}",
                    item.Patrimonio, item.Categoria, item.Subcategoria, item.Localidade, item.Status, item.ID);
                new NpgsqlCommand(sql, dbConn).ExecuteNonQuery();
                dbConn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("INVENTARIO/ItemDAO/Update:: " + e);
                dbConn.Close();
                return false;
            }
        }

        public bool Delete(int ID)
        {
            try
            {
                NpgsqlConnection dbConn = Database.Conexao;
                string sql = String.Format("DELETE FROM item WHERE id = {0}",
                    ID);
                new NpgsqlCommand(sql, dbConn).ExecuteNonQuery();
                dbConn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("INVENTARIO/ItemDAO/Delete:: " + e);
                dbConn.Close();
                return false;
            }
        }

        public bool AlteraLocalidade(int id, int localidade)
        {
            try
            {
                NpgsqlConnection dbConn = Database.Conexao;
                string sql = String.Format("UPDATE item(Localidade) values({0}) WHERE id = {1}",
                    localidade, id);
                new NpgsqlCommand(sql, dbConn).ExecuteNonQuery();
                dbConn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("INVENTARIO/ItemDAO/AlteraLocalidade:: " + e);
                dbConn.Close();
                return false;
            }
        }

        public bool AlteraStatus(int id, int status)
        {
            try
            {
                NpgsqlConnection dbConn = Database.Conexao;
                string sql = String.Format("UPDATE item(Status) values({0}) WHERE id = {1}",
                    status, id);
                new NpgsqlCommand(sql, dbConn).ExecuteNonQuery();
                dbConn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("INVENTARIO/ItemDAO/AlteraStatus:: " + e);
                dbConn.Close();
                return false;
            }
        }
    }
}