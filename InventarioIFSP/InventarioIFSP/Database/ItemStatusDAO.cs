using InventarioIFSP.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioIFSP.Database
{
    public class ItemStatusDAO
    {
        private static NpgsqlConnection dbConn;
        public static List<SelectListItem> lista_status;

        // Cria objeto de conexão, se já existir abre a conexão
        private static void OpenConn()
        {
            if (dbConn == null)
            {
                dbConn = Database.Conexao;
            }
            else
            {
                if (dbConn.State != ConnectionState.Open)
                    dbConn.Open();
            }
        }

        public static object Create(ItemStatus item)
        {
            NpgsqlParameter param;
            if (dbConn == null)
                dbConn = Database.Conexao;
            else
                dbConn.Open();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "INSERT INTO item_status(nome, descricao) values (@nome, @descricao )RETURNING id";

                param = new NpgsqlParameter("@nome", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
                param.Value = item.Nome;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@descricao", NpgsqlTypes.NpgsqlDbType.Varchar, 255);
                param.Value = item.Descricao;
                command.Parameters.Add(param);

                command.Prepare();
                var result = command.ExecuteScalar();
                dbConn.Close();

                return result;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/ItemStatusDAO/Create:: " + e);
                dbConn.Close();
                return null;
            }
        }

        public static Boolean Update(ItemStatus item)
        {
            NpgsqlParameter param;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "UPDATE item_status SET nome = @nome, descricao = @descricao WHERE id =@id  RETURNING id";

                param = new NpgsqlParameter("@nome", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
                param.Value = item.Nome;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@descricao", NpgsqlTypes.NpgsqlDbType.Varchar, 255);
                param.Value = item.Descricao;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = item.Id;
                command.Parameters.Add(param);

                command.Prepare();

                var result = command.ExecuteScalar();
                dbConn.Close();

                if (result != null && (int)result > 0)
                    return true;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/ItemStatusDAO/Update:: " + e);

                dbConn.Close();
            }
            return false;
        }
        
        public static Boolean Delete(int Id)
        {
            NpgsqlParameter param;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "DELETE FROM item_status WHERE id = @id RETURNING 0";

                param = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = Id;
                command.Parameters.Add(param);

                var result = command.ExecuteScalar();

                dbConn.Close();
                if ((int)result == 0)
                    return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/ItemStatusDAO/Delete:: " + e);
                dbConn.Close();
            }
            return false;
        }

        public static List<ItemStatus> GetAll()
        {
            DataTable table = new DataTable();
            List<ItemStatus> lista = new List<ItemStatus>();
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "SELECT * FROM item_status";
                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        lista.Add(
                            new ItemStatus
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Nome = dr["nome"].ToString(),
                                Descricao = dr["descricao"].ToString()

                            }
                        );
                    }
                }
                dbConn.Close();
                return lista;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/ItemStatusDAO/GetAll:: " + e);
                dbConn.Close();
                return null;
            }


        }

        public static ItemStatus GetByID(int id)
        {
            DataTable table = new DataTable();
            ItemStatus status = null;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "SELECT * FROM item_status WHERE id = " + id;
                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        status = new ItemStatus
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Nome = dr["nome"].ToString(),
                            Descricao = dr["descricao"].ToString()
                        };
                        dbConn.Close();
                        return status;
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/ItemStatusDAO/GetAll:: " + e);
            }
            dbConn.Close();
            return status;


        }

        // Retorna lista de todos status
        public static void AtualizaTiposStatus()
        {
            lista_status = new List<SelectListItem>();
            List<ItemStatus> lista = GetAll();
            
            foreach (ItemStatus status in lista)
            {
                lista_status.Add(
                    new SelectListItem
                    {
                        Text = status.Nome,
                        Value = status.Id.ToString()
                    }
                );
            }
        }
    }
}