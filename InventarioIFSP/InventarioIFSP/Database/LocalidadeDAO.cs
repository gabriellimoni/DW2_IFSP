using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Data;
using InventarioIFSP.Models;
using System.Web.Mvc;

namespace InventarioIFSP.Database
{
    public static class LocalidadeDAO
    {
        private static NpgsqlConnection dbConn;

        public static List<SelectListItem> lista_localidades;

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

        public static object Create(Localidade local)
        {
            NpgsqlParameter param;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "INSERT INTO localidade(nome, categoria, bloco)" +
                    "values(@nome, @categoria, @bloco) RETURNING id";
                
                param = new NpgsqlParameter("@nome", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
                param.Value = local.Nome;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@categoria", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = local.Categoria.ID;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@bloco", NpgsqlTypes.NpgsqlDbType.Varchar, 20);
                param.Value = local.Bloco;
                command.Parameters.Add(param);

                command.Prepare();
                var result = command.ExecuteScalar();
                dbConn.Close();

                if (result != null && (int)result > 0)
                    return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/LocalidadeDAO/Create:: " + e);
            }
            dbConn.Close();
            return null;
        }

        public static bool Update(Localidade local)
        {
            NpgsqlParameter param;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "UPDATE localidade SET nome = @nome, categoria = @categoria, bloco = @bloco " + 
                    "WHERE id = @id RETURNING id";

                param = new NpgsqlParameter("@nome", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
                param.Value = local.Nome;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@categoria", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = local.Categoria.ID;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@bloco", NpgsqlTypes.NpgsqlDbType.Varchar, 20);
                param.Value = local.Bloco;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = local.ID;
                command.Parameters.Add(param);

                command.Prepare();
                var result = command.ExecuteScalar();
                dbConn.Close();

                if (result != null && (int)result > 0)
                    return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/LocalidadeDAO/Update:: " + e);
            }
            dbConn.Close();
            return false;
        }

        public static bool Delete(int Id)
        {
            NpgsqlParameter param;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "DELETE FROM localidade WHERE id = @id RETURNING 0";

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
                System.Diagnostics.Debug.WriteLine("INVENTARIO/LocalidadeDAO/Delete:: " + e);
                dbConn.Close();
            }
            return false;
        }

        public static List<Localidade> GetAll()
        {
            DataTable table = new DataTable();
            List<Localidade> localidades = new List<Localidade>();
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "SELECT * FROM localidade";
                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        localidades.Add(
                            new Localidade
                            {
                                ID = Convert.ToInt32(dr["id"]),
                                Nome = dr["nome"].ToString(),
                                Bloco = dr["bloco"].ToString(),
                                Categoria = new LocalidadeCategoria
                                {
                                    ID = Convert.ToInt32(dr["categoria"]),
                                }

                            }
                        );
                    }
                }
                foreach(Localidade local in localidades)
                {
                    local.Categoria = LocalidadeCategoriaDAO.GetByID(local.Categoria.ID);
                }
                dbConn.Close();
                return localidades;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/LocalidadeDAO/GetAll:: " + e);
                dbConn.Close();
                return null;
            }

        }

        public static Localidade GetByID(int Id)
        {
            NpgsqlParameter param;
            DataTable table = new DataTable();
            Localidade local = null;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "SELECT * FROM localidade WHERE id = @id";

                param = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = Id;
                command.Parameters.Add(param);

                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        local = new Localidade
                        {
                            ID = Convert.ToInt32(dr["id"]),
                            Nome = dr["nome"].ToString(),
                            Bloco = dr["bloco"].ToString(),
                            Categoria = new LocalidadeCategoria { ID = Convert.ToInt32(dr["categoria"])}
                        };
                        dbConn.Close();
                        return local;
                    }

                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/LocalidadeDAO/GetByID:: " + e);
            }
            dbConn.Close();
            return null;

        }

        public static List<Localidade> GetByBlock(string block)
        {
            DataTable table = new DataTable();
            List<Localidade> localidades = new List<Localidade>();
            NpgsqlParameter param;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "SELECT * FROM localidade WHERE bloco = @bloco";

                param = new NpgsqlParameter("@bloco", NpgsqlTypes.NpgsqlDbType.Varchar, 20);
                param.Value = block;
                command.Parameters.Add(param);

                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        localidades.Add(
                            new Localidade
                            {
                                ID = Convert.ToInt32(dr["id"]),
                                Nome = dr["nome"].ToString(),
                                Bloco = dr["bloco"].ToString(),
                                Categoria = new LocalidadeCategoria
                                {
                                    ID = Convert.ToInt32(dr["categoria"])
                                }
                               

                            }
                        );
                    }
                }
                dbConn.Close();
                return localidades;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/LocalidadeDAO/GetByBlock:: " + e);
                dbConn.Close();
                return null;
            }
        }

        public static List<Localidade> GetByCategory(LocalidadeCategoria cat)
        {
            DataTable table = new DataTable();
            List<Localidade> localidades = new List<Localidade>();
            NpgsqlParameter param;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "SELECT * FROM localidade WHERE categoria = @categoria";

                param = new NpgsqlParameter("@categoria", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = cat.ID;
                command.Parameters.Add(param);

                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        localidades.Add(
                            new Localidade
                            {
                                ID = Convert.ToInt32(dr["id"]),
                                Nome = dr["nome"].ToString(),
                                Bloco = dr["bloco"].ToString(),
                                Categoria = new LocalidadeCategoria
                                {
                                    ID = Convert.ToInt32(dr["categoria"])
                                }
                            }
                        );
                    }
                }
                dbConn.Close();
                return localidades;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/LocalidadeDAO/GetByCategory:: " + e);
                dbConn.Close();
                return null;
            }
        }

       
        public static void AtualizaLocalidades()
        {
            lista_localidades = new List<SelectListItem>();
            List<Localidade> lista = GetAll();

            foreach (Localidade localidade in lista)
            {
                lista_localidades.Add(
                    new SelectListItem
                    {
                        Text = localidade.Nome,
                        Value = localidade.ID.ToString()
                    }
                );
            }
            
        }

        public static List<SelectListItem> GetSelectList()
        {
            List<Localidade> localidades = GetAll();
            List<SelectListItem> lista = new List<SelectListItem>();
            foreach( Localidade local in localidades)
            {
                lista.Add(new SelectListItem
                {
                    Value = local.ID.ToString(),
                    Text = local.Nome + " - " + local.Bloco
                });
            }
            return lista;
        }

    }
}