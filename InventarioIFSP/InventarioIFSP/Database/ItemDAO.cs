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
    public class ItemDAO
    {
        private static NpgsqlConnection dbConn;

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

        public static object Create(Item Item)
        {
            NpgsqlParameter param;
            if (dbConn == null)
                dbConn = Database.Conexao;
            else
                dbConn.Open();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "INSERT INTO item(patrimonio, categoria, localidade, observacao, status)" +
                    "values(@patrimonio, @categoria, @localidade, @observacao, @status) RETURNING id";



                param = new NpgsqlParameter("@patrimonio", NpgsqlTypes.NpgsqlDbType.Varchar, 20);
                param.Value = Item.Patrimonio;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@categoria", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = Item.Categoria.ID;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@localidade", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = Item.Localidade.ID;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@observacao", NpgsqlTypes.NpgsqlDbType.Varchar, 255);
                param.Value = Item.Observacao;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@status", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = Item.Status.Id;
                command.Parameters.Add(param);

                command.Prepare();
                var result = command.ExecuteScalar();
                dbConn.Close();

                return result;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/ItemDAO/Create:: " + e);
                dbConn.Close();
                return null;
            }
        }

        
        public static Boolean Update(Item Item)
        {
            NpgsqlParameter param;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "UPDATE item SET"
                    + " patrimonio = @patrimonio, categoria = @categoria,"
                    + " localidade = @localidade, observacao = @observacao, status=@status"
                    + " WHERE id =@id  RETURNING id";

                param = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = Item.ID;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@patrimonio", NpgsqlTypes.NpgsqlDbType.Varchar, 20);
                param.Value = Item.Patrimonio;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@categoria", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = Item.Categoria.ID;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@localidade", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = Item.Localidade.ID;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@observacao", NpgsqlTypes.NpgsqlDbType.Varchar, 255);
                param.Value = Item.Observacao;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@status", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = Item.Status.Id;
                command.Parameters.Add(param);

                command.Prepare();

                var result = command.ExecuteScalar();
                dbConn.Close();

                if (result != null && (int)result > 0)
                    return true;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/ItemDAO/Update:: " + e);

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
                command.CommandText = "DELETE FROM item WHERE id = @id RETURNING 0";

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
                System.Diagnostics.Debug.WriteLine("INVENTARIO/ItemDAO/Delete:: " + e);
                dbConn.Close();
            }
            return false;
        }

        public static Item GetByID(int Id)
        {
            NpgsqlParameter param;
            DataTable table = new DataTable();
            Item Item = null;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "SELECT * FROM Item WHERE id = @id";

                param = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = Id;
                command.Parameters.Add(param);

                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        Item = new Item
                        {
                            ID = Convert.ToInt32(dr["id"]),
                            Patrimonio = dr["patrimonio"].ToString(),
                            Localidade = new Localidade { ID = Convert.ToInt32(dr["localidade"]) },
                            Categoria = new ItemCategoria { ID = Convert.ToInt32(dr["categoria"]) },
                            Observacao = dr["observacao"].ToString(),
                            Status = new ItemStatus { Id = Convert.ToInt32(dr["status"]) }
                        };
                        Item.Categoria = ItemCategoriaDAO.GetByID(Item.Categoria.ID);
                        Item.Status = ItemStatusDAO.GetByID(Item.Status.Id);
                        Item.Localidade = LocalidadeDAO.GetByID(Item.Localidade.ID);
                        return Item;
                    }

                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/ItemDAO/GetByID:: " + e);
            }
            dbConn.Close();
            return null;
        }

        public static List<Item> GetByLocalidade(int id_localidade)
        {
            DataTable table = new DataTable();
            List<Item> Itens = new List<Item>();
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                NpgsqlParameter param;
                command.CommandText = "SELECT * FROM Item WHERE localidade = @id_localidade";

                param = new NpgsqlParameter("@id_localidade", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = id_localidade;
                command.Parameters.Add(param);


                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        Itens.Add(
                            new Item
                            {
                                ID = Convert.ToInt32(dr["id"]),
                                Patrimonio = dr["patrimonio"].ToString(),
                                Localidade = new Localidade { ID = Convert.ToInt32(dr["localidade"]) },
                                Categoria = new ItemCategoria { ID = Convert.ToInt32(dr["categoria"]) },
                                Observacao = dr["observacao"].ToString(),
                                Status = new ItemStatus { Id = Convert.ToInt32(dr["status"]) }
                            }
                        );
                    }
                }
                foreach (Item item in Itens)
                {
                    item.Categoria = ItemCategoriaDAO.GetByID(item.Categoria.ID);
                }
                foreach (Item item in Itens)
                {
                    item.Localidade = LocalidadeDAO.GetByID(item.Localidade.ID);
                }
                foreach (Item item in Itens)
                {
                    item.Status = ItemStatusDAO.GetByID(item.Status.Id);
                }
                dbConn.Close();
                return Itens;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/ItemDAO/GetByLocalidade:: " + e);
            }
            dbConn.Close();
            return null;
        }

        public static List<Item> GetAll()
        {
            DataTable table = new DataTable();
            List<Item> Itens = new List<Item>();
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "SELECT * FROM Item";
                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        Itens.Add(
                            new Item
                            {
                                ID = Convert.ToInt32(dr["id"]),
                                Patrimonio = dr["patrimonio"].ToString(),
                                Localidade = new Localidade { ID = Convert.ToInt32(dr["localidade"]) },
                                Categoria = new ItemCategoria { ID = Convert.ToInt32(dr["categoria"]) },
                                Observacao = dr["observacao"].ToString(),
                                Status = new ItemStatus { Id = Convert.ToInt32(dr["status"]) }
                            }
                        );
                    }
                }
                foreach( Item item in Itens)
                {
                    item.Categoria = ItemCategoriaDAO.GetByID(item.Categoria.ID);
                }
                foreach (Item item in Itens)
                {
                    item.Localidade = LocalidadeDAO.GetByID(item.Localidade.ID);
                }
                foreach (Item item in Itens)
                {
                    item.Status = ItemStatusDAO.GetByID(item.Status.Id);
                }
                dbConn.Close();
                return Itens;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/ItemDAO/GetAll:: " + e);
                dbConn.Close();
                return null;
            }


        }

       

    }
}