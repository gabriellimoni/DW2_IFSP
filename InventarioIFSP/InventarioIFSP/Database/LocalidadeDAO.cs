using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Data;
using InventarioIFSP.Models;

namespace InventarioIFSP.Database
{
    public class LocalidadeDAO
    {
        private NpgsqlConnection dbConn;
        public LocalidadeDAO()
        {
            dbConn = Database.Conexao;
        }
        
        public bool Create(Localidade local)
        {
            try
            {
                string sql = String.Format("INSERT INTO localidade(nome, categoria, bloco) values('{0}',{1},'{2}')",
                    local.Nome, local.Categoria.ID, local.Bloco);

                new NpgsqlCommand(sql, dbConn).ExecuteNonQuery();
                dbConn.Close();
                return true;
            }catch(Exception e)
            {
                Console.WriteLine("INVENTARIO/LocalidadeDAO/Create:: " + e);
                dbConn.Close();
                return false;
            }
        }

        public bool Update(Localidade local)
        {
            try
            {
                NpgsqlConnection dbConn = Database.Conexao;
                string sql = String.Format("UPDATE localidade(nome, categoria, bloco) values('{0}',{1},'{2}') WHERE id ={3}",
                    local.Nome, local.Categoria.ID, local.Bloco, local.ID);

                new NpgsqlCommand(sql, dbConn).ExecuteNonQuery();
                dbConn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("INVENTARIO/LocalidadeDAO/Update:: " + e);
                dbConn.Close();
                return false;
            }
        }

        public bool Delete(int Id)
        {
            try
            {
                NpgsqlConnection dbConn = Database.Conexao;
                string sql = String.Format("DELETE FROM localidade WHERE id ={1}", Id);
                new NpgsqlCommand(sql, dbConn).ExecuteNonQuery();
                dbConn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("INVENTARIO/LocalidadeDAO/Delete:: " + e);
                dbConn.Close();
                return false;
            }
        }

        public DataTable GetByID(int Id)
        {
            DataTable table = new DataTable();
            try
            {
                string sql = "SELECT * FROM localidade WHERE id = " + Id;
                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(sql, dbConn);
                Adpt.Fill(table);
                dbConn.Close();
                return table;
            }
            catch (Exception e)
            {
                Console.WriteLine("INVENTARIO/LocalidadeDAO/GetByID:: " + e);
                return null;
            }

        }

        public DataTable GetByBlock(string block)
        {
            DataTable table = new DataTable();
            try
            {
                string sql = "SELECT * FROM localidade WHERE block = '" + block + "'";
                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(sql, dbConn);
                Adpt.Fill(table);
                dbConn.Close();
                return table;
            }
            catch (Exception e)
            {
                Console.WriteLine("INVENTARIO/LocalidadeDAO/GetByBlock:: " + e);
                return null;
            }
        }

        public DataTable GetByCategory(LocalidadeCategoria cat)
        {
            DataTable table = new DataTable();
            try
            {
                string sql = "SELECT * FROM localidade WHERE categoria = '" + cat + "'";
                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(sql, dbConn);
                Adpt.Fill(table);
                dbConn.Close();
                return table;
            }
            catch (Exception e)
            {
                Console.WriteLine("INVENTARIO/LocalidadeDAO/GetByCategory:: " + e);
                return null;
            }
        }

    }
}