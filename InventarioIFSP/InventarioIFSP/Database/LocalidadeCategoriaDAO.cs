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
    public class LocalidadeCategoriaDAO
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
        public static object Create(LocalidadeCategoria categoria)
        {
            NpgsqlParameter param;
            if (dbConn == null)
                dbConn = Database.Conexao;
            else
                dbConn.Open();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "INSERT INTO localidade_categoria(nome, descricao) values (@nome, @descricao) RETURNING id";

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
                System.Diagnostics.Debug.WriteLine("INVENTARIO/LocalidadeCategoriaDAO/Create:: " + e);
                dbConn.Close();
                return null;
            }
        }

        public static Boolean Update(LocalidadeCategoria categoria)
        {
            NpgsqlParameter param;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "UPDATE localidade_categoria SET nome = @nome, descricao = @descricao WHERE id =@id  RETURNING id";

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
                System.Diagnostics.Debug.WriteLine("INVENTARIO/LocalidadeCategoriaDAO/Update:: " + e);

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
                command.CommandText = "DELETE FROM localidade_categoria WHERE id = @id RETURNING 0";

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
                System.Diagnostics.Debug.WriteLine("INVENTARIO/LocalidadeCategoriaDAO/Delete:: " + e);
                dbConn.Close();
            }
            return false;
        }

        public static LocalidadeCategoria GetByID(int id)
        {
            DataTable table = new DataTable();
            LocalidadeCategoria categoria = null;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "SELECT * FROM localidade_categoria WHERE id = " + id;
                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        categoria =  new LocalidadeCategoria
                        {
                            ID = Convert.ToInt32(dr["id"]),
                            Nome = dr["nome"].ToString(),
                            Descricao = dr["descricao"].ToString()

                        };
                        dbConn.Close();
                        return categoria;
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/LocalidadeCategoriaDAO/GetAll:: " + e);
            }
            dbConn.Close();
            return null;
        }

        public static List<LocalidadeCategoria> GetAll()
        {
            DataTable table = new DataTable();
            List<LocalidadeCategoria> lista = new List<LocalidadeCategoria>();
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "SELECT * FROM localidade_categoria";
                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        lista.Add(
                            new LocalidadeCategoria
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
                System.Diagnostics.Debug.WriteLine("INVENTARIO/LocalidadeCategoriaDAO/GetAll:: " + e);
                dbConn.Close();
                return null;
            }            
        }

        public static void AtualizaCategorias()
        {
            lista_categorias = new List<SelectListItem>();
            List<LocalidadeCategoria> lista = GetAll();

            foreach (LocalidadeCategoria cat in lista)
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
