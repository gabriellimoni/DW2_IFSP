using Inventario_IFSPPRC.Models;
using InventarioIFSP.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace InventarioIFSP.Database
{
    public class UsuarioDAO
    {
        private NpgsqlConnection dbConn;

        public UsuarioDAO()
        {
            dbConn = Database.Conexao;
        }

        public bool Create(Usuario usuario)
        {
            try
            {
                string sql = String.Format("INSERT INTO Usuario(nome, email, senha, nivel) values('{0}','{1}','{2}', {3})",
                    usuario.Nome, usuario.Email, usuario.Senha, 2);

                new NpgsqlCommand(sql, dbConn).ExecuteNonQuery();
                dbConn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("INVENTARIO/UsuarioDAO/Create:: " + e);
                dbConn.Close();
                return false;
            }
        }

        public bool Update(Usuario usuario)
        {
            try
            {
                NpgsqlConnection dbConn = Database.Conexao;
                string sql = String.Format("UPDATE Usuario(nome, email, senha) values('{0}','{1}','{2}') WHERE id = {3}",
                    usuario.Nome, usuario.Email, usuario.Senha, usuario.ID);

                new NpgsqlCommand(sql, dbConn).ExecuteNonQuery();
                dbConn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("INVENTARIO/UsuarioDAO/Update:: " + e);
                dbConn.Close();
                return false;
            }
        }

        public bool UpdateNivel(int id, int novo_nivel)
        {
            try
            {
                NpgsqlConnection dbConn = Database.Conexao;
                string sql = String.Format("UPDATE Usuario(nivel) values({0}) WHERE id = {1}",
                   novo_nivel, id);

                new NpgsqlCommand(sql, dbConn).ExecuteNonQuery();
                dbConn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("INVENTARIO/UsuarioDAO/UpdateNivel:: " + e);
                dbConn.Close();
                return false;
            }
        }

        public bool UpdatePassword(Usuario usuario)
        {
            try
            {
                NpgsqlConnection dbConn = Database.Conexao;
                string sql = String.Format("UPDATE Usuario(password) values('{0}') WHERE id = {1}",
                   usuario.Senha, usuario.ID);

                new NpgsqlCommand(sql, dbConn).ExecuteNonQuery();
                dbConn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("INVENTARIO/UsuarioDAO/UpdatePassword:: " + e);
                dbConn.Close();
                return false;
            }
        }

        public bool Delete(int Id)
        {
            try
            {
                NpgsqlConnection dbConn = Database.Conexao;
                string sql = String.Format("DELETE FROM Usuario WHERE id ={1}", Id);
                new NpgsqlCommand(sql, dbConn).ExecuteNonQuery();
                dbConn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("INVENTARIO/UsuarioDAO/Delete:: " + e);
                dbConn.Close();
                return false;
            }
        }

        public Usuario LoginViaProntuario(Usuario usuario)
        {
            DataTable table = new DataTable();
            try
            {
                string sql = String.Format("SELECT * FROM Usuario WHERE prontuario = '{0}' AND senha = '{1}'",usuario.Prontuario, usuario.Senha);
                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(sql, dbConn);
                Adpt.Fill(table);
                
                if(table.Rows.Count > 0)
                {
                    DataRow dr = table.NewRow();
                    usuario = new Usuario
                    {
                        ID = Convert.ToInt32(dr["id"]),
                        Nome = dr["nome"].ToString(),
                        Email = dr["email"].ToString(),
                        Prontuario = dr["prontuario"].ToString(),
                        Nivel = Convert.ToInt32(dr["nivel"])    
                    };
                    dbConn.Close();
                    return usuario;
                }               
                dbConn.Close();
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("INVENTARIO/UsuarioDAO/LoginViaProntuario:: " + e);
                return null;
            }
        }

        public Usuario LoginViaEmail(Usuario usuario)
        {
            DataTable table = new DataTable();
            try
            {
                string sql = String.Format("SELECT * FROM Usuario WHERE email = '{0}' AND senha = '{1}'", usuario.Email, usuario.Senha);
                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(sql, dbConn);
                Adpt.Fill(table);
                if (table.Rows.Count > 0)
                {
                    DataRow dr = table.NewRow();
                    usuario = new Usuario
                    {
                        ID = Convert.ToInt32(dr["id"]),
                        Nome = dr["nome"].ToString(),
                        Email = dr["email"].ToString(),
                        Prontuario = dr["prontuario"].ToString(),
                        Nivel = Convert.ToInt32(dr["nivel"])
                    };
                    dbConn.Close();
                    return usuario;
                }
                dbConn.Close();
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("INVENTARIO/UsuarioDAO/LoginViaEmail:: " + e);
                return null;
            }
        }

        public DataTable GetByID(int Id)
        {
            DataTable table = new DataTable();
            try
            {
                string sql = "SELECT * FROM Usuario WHERE id = " + Id;
                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(sql, dbConn);
                Adpt.Fill(table);
                dbConn.Close();
                return table;
            }
            catch (Exception e)
            {
                Console.WriteLine("INVENTARIO/UsuarioDAO/GetByID:: " + e);
                return null;
            }
        }

        public List<Usuario> GetAll()
        {
            List<Usuario> lista = new List<Usuario>();
            DataTable table = new DataTable();
            try
            {
                string sql = "SELECT * FROM Usuario";
                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(sql, dbConn);
                Adpt.Fill(table);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        lista.Add(
                            new Usuario
                            {
                                ID = Convert.ToInt32(dr["id"]),
                                Nome = dr["nome"].ToString(),
                                Email = dr["email"].ToString(),
                                Prontuario = dr["prontuario"].ToString(),
                                Nivel = Convert.ToInt32(dr["nivel"])
                            }
                        );                        
                    }
                    
                    dbConn.Close();
                    return lista;
                }
                dbConn.Close();
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("INVENTARIO/UsuarioDAO/GetAll:: " + e);
                return null;
            }

        }
    }
}