using InventarioIFSP.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace InventarioIFSP.Database
{
    public class InventarioDAO
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

        // Consolida o inventario Semestre/Ano
        public static Boolean Create()
        {
            int semestre =1;
            if (DateTime.Now.Month > 6)
            {
                semestre = 2;
            }
            Inventario inventario = new Inventario
            {
                Ano = DateTime.Now.Year,
                Semestre = semestre,
                Consolidado  = "SIM"
            };
            NpgsqlParameter param;
            if (dbConn == null)
                dbConn = Database.Conexao;
            else
                dbConn.Open();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "INSERT INTO Inventario(semestre, ano, consolidado)" +
                    "values(@semestre, @ano, @consolidado) RETURNING id";

                param = new NpgsqlParameter("@semestre", NpgsqlTypes.NpgsqlDbType.Integer, 00);
                param.Value = inventario.Semestre;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@ano", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = inventario.Ano;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@consolidado", NpgsqlTypes.NpgsqlDbType.Varchar, 10);
                param.Value = inventario.Consolidado;
                command.Parameters.Add(param);

                command.Prepare();
                var result = command.ExecuteScalar();
                dbConn.Close();

                List<Localidade> localidades = LocalidadeDAO.GetAll();
                foreach(Localidade local in localidades)
                {
                    List<Item> itens = ItemDAO.GetByLocalidade(local.ID);
                    if (!InsereItensInventario(itens, Convert.ToInt32(result)))
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/InventarioDAO/Create:: " + e);
                dbConn.Close();
                return false;
            }
        }

        private static Boolean InsereItensInventario(List<Item> itens, int inventario)
        {
            NpgsqlParameter param;
            if (dbConn == null)
                dbConn = Database.Conexao;
            else
                dbConn.Open();
            try
            {
                foreach(Item item in itens)
                {
                    NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                    command.CommandText = "INSERT INTO item_inventario(patrimonio, inventario, categoria, localidade, bloco, status)" +
                        "values(@patrimonio, @inventario, @categoria, @localidade, @bloco, @status) RETURNING id";

                    param = new NpgsqlParameter("@patrimonio", NpgsqlTypes.NpgsqlDbType.Varchar, 20);
                    param.Value = item.Patrimonio;
                    command.Parameters.Add(param);

                    param = new NpgsqlParameter("@inventario", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                    param.Value = inventario;
                    command.Parameters.Add(param);

                    param = new NpgsqlParameter("@categoria", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
                    param.Value = item.Categoria.Nome;
                    command.Parameters.Add(param);

                    param = new NpgsqlParameter("@localidade", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
                    param.Value = item.Localidade.Nome;
                    command.Parameters.Add(param);

                    param = new NpgsqlParameter("@bloco", NpgsqlTypes.NpgsqlDbType.Varchar, 20);
                    param.Value = item.Localidade.Bloco;
                    command.Parameters.Add(param);

                    param = new NpgsqlParameter("@status", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
                    param.Value = item.Status.Nome;
                    command.Parameters.Add(param);

                    command.Prepare();
                    command.ExecuteScalar();

                }
                dbConn.Close();
                return true;
            }
            catch (Exception err)
            {

                System.Diagnostics.Debug.WriteLine("INVENTARIO/InventarioDAO/InsereItensInventario:: " + err);
                dbConn.Close();
                return false;
            }
        }

        public static Boolean Delete(int Id)
        {
            NpgsqlParameter param;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "DELETE FROM Inventario WHERE id = @id RETURNING 0";

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
                System.Diagnostics.Debug.WriteLine("INVENTARIO/InventarioDAO/Delete:: " + e);
                dbConn.Close();
            }
            return false;
        }
        
        public static Inventario GetByID(int Id)
        {
            NpgsqlParameter param;
            DataTable table = new DataTable();
            Inventario Inventario = null;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "SELECT * FROM Inventario WHERE id = @id";

                param = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = Id;
                command.Parameters.Add(param);

                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        Inventario = new Inventario
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Semestre = Convert.ToInt32(dr["semestre"]),
                            Ano = Convert.ToInt32(dr["ano"]),
                            Consolidado = dr["consolidado"].ToString()
                        };
                        return Inventario;
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/InventarioDAO/GetByID:: " + e);
            }
            dbConn.Close();
            return null;
        }

        public static Inventario GetByYearSemester(int ano, int semestre)
        {
            NpgsqlParameter param;
            DataTable table = new DataTable();
            Inventario Inventario = null;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "SELECT * FROM Inventario WHERE semestre = @semestre AND ano = @ano";

                param = new NpgsqlParameter("@semestre", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = semestre;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@ano", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = ano;
                command.Parameters.Add(param);

                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        Inventario = new Inventario
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Semestre = Convert.ToInt32(dr["semestre"]),
                            Ano = Convert.ToInt32(dr["ano"]),
                            Consolidado = dr["consolidado"].ToString()
                        };
                        return Inventario;
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/InventarioDAO/GetByID:: " + e);
            }
            dbConn.Close();
            return null;
        }

        // Verifica que se já existe inventário do semestre atual
        // Se já estiver, retorna true
        public static Boolean CheckStatus()
        {
            int ano = DateTime.Now.Year;
            int semestre = 1;
            if (DateTime.Now.Month > 6)
                semestre = 2;

            NpgsqlParameter param;
            DataTable table = new DataTable();
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "SELECT * FROM inventario WHERE ano = @ano and semestre = @semestre";

                param = new NpgsqlParameter("@ano", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = ano;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@semestre", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = semestre;
                command.Parameters.Add(param);

                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);

                if (table.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/InventarioDAO/CheckStatus:: " + e);
            }
            dbConn.Close();
            return false;
        }

        public static List<Inventario> GetAll()
        {
            DataTable table = new DataTable();
            List<Inventario> Inventarios = new List<Inventario>();
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "SELECT * FROM Inventario";
                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        Inventarios.Add(
                            new Inventario
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Semestre = Convert.ToInt32(dr["semestre"]),
                                Ano = Convert.ToInt32(dr["ano"]),
                                Consolidado = dr["consolidado"].ToString()
                            }
                        );
                    }
                }
                dbConn.Close();
                return Inventarios;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/InventarioDAO/GetAll:: " + e);
                dbConn.Close();
                return null;
            }


        }

        public static List<ItemInventario> GetItens(int id)
        {
            DataTable table = new DataTable();
            List<ItemInventario> Itens = new List<ItemInventario>();
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "SELECT * FROM item_inventario where inventario = @inventario";
                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        Itens.Add(
                            new ItemInventario
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Inventario = Convert.ToInt32(dr["inventario"]),
                                Patrimonio = dr["patrimonio"].ToString(),
                                Categoria = dr["categoria"].ToString(),
                                Localidade = dr["localidade"].ToString(),
                                Bloco = dr["bloco"].ToString(),
                                Status = dr["status"].ToString()
                            }
                        );
                    }
                }
                dbConn.Close();
                return Itens;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/InventarioDAO/GetItens:: " + e);
                dbConn.Close();
                return null;
            }


        }
    }
}