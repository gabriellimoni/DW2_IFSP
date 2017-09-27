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
    public class ItemCategoriaDAO
    {
        private static NpgsqlConnection dbConn;
        public static List<SelectListItem> lista_categorias;

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

        // Cria subcategoria
        public static object Create(ItemCategoria categoria)
        {
            NpgsqlParameter param;
            if (dbConn == null)
                dbConn = Database.Conexao;
            else
                dbConn.Open();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "INSERT INTO item_categoria(nome, descricao) values (@nome, @descricao) RETURNING id";

                param = new NpgsqlParameter("@nome", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
                param.Value = categoria.Nome;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@descricao", NpgsqlTypes.NpgsqlDbType.Varchar, 255);
                param.Value = categoria.Descricao;
                command.Parameters.Add(param);

                command.Prepare();
                var result = command.ExecuteScalar();
                dbConn.Close();

                return result;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/ItemCategoriaDAO/Create:: " + e);
                dbConn.Close();
                return null;
            }
        }

        public static Boolean Update(ItemCategoria categoria)
        {
            NpgsqlParameter param;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "UPDATE item_categoria SET nome = @nome, descricao = @descricao WHERE id =@id  RETURNING id";

                param = new NpgsqlParameter("@nome", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
                param.Value = categoria.Nome;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@descricao", NpgsqlTypes.NpgsqlDbType.Varchar, 255);
                param.Value = categoria.Descricao;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = categoria.ID;
                command.Parameters.Add(param);

                command.Prepare();

                var result = command.ExecuteScalar();
                dbConn.Close();

                if (result != null && (int)result > 0)
                    return true;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/ItemCategoriaDAO/Update:: " + e);

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
                command.CommandText = "DELETE FROM item_categoria WHERE id = @id RETURNING 0";

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
                System.Diagnostics.Debug.WriteLine("INVENTARIO/ItemCategoriaDAO/Delete:: " + e);
                dbConn.Close();
            }
            return false;
        }

        public static ItemCategoria GetByID(int id)
        {
            DataTable table = new DataTable();
            ItemCategoria cat = null;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "SELECT * FROM item_categoria WHERE id = " + id;
                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        cat= new ItemCategoria
                        {
                            ID = Convert.ToInt32(dr["id"]),
                            Nome = dr["nome"].ToString(),
                            Descricao = dr["descricao"].ToString()
                        };
                    dbConn.Close();
                    return cat;
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/ItemCategoriaDAO/GetAll:: " + e);
            }
            dbConn.Close();
            return cat;


        }

        public static List<ItemCategoria> GetAll()
        {
            DataTable table = new DataTable();
            List<ItemCategoria> lista = new List<ItemCategoria>();
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "SELECT * FROM item_categoria";
                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        lista.Add(
                            new ItemCategoria
                            {
                                ID = Convert.ToInt32(dr["id"]),
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
                System.Diagnostics.Debug.WriteLine("INVENTARIO/ItemCategoriaDAO/GetAll:: " + e);
                dbConn.Close();
                return null;
            }


        }

        public static void AtualizaCategorias()
        {
            lista_categorias = new List<SelectListItem>();
            List<ItemCategoria> lista = GetAll();

            foreach (ItemCategoria cat in lista)
            {
                lista_categorias.Add(
                    new SelectListItem
                    {
                        Text = cat.Nome,
                        Value = cat.ID.ToString()
                    }
                );
            }
        }
    }
}