using Inventario_IFSPPRC.Models;
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
    public static class UsuarioDAO
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
                if(dbConn.State != ConnectionState.Open)
                    dbConn.Open();
            }
        }

        // Cria usuário
        public static object Create(Usuario usuario)
        {
            NpgsqlParameter param;
            if (dbConn == null)
                dbConn = Database.Conexao;
            else
                dbConn.Open();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "INSERT INTO Usuario(nome, email, prontuario, senha)" +
                    "values(@nome, @email, @prontuario, @senha) RETURNING id";


                param = new NpgsqlParameter("@nome", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
                param.Value = usuario.Nome;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@email", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
                param.Value = usuario.Email;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@prontuario", NpgsqlTypes.NpgsqlDbType.Varchar, 20);
                param.Value = usuario.Prontuario;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@senha", NpgsqlTypes.NpgsqlDbType.Varchar, 255);
                param.Value = usuario.Senha;
                command.Parameters.Add(param);

                command.Prepare();
                var result = command.ExecuteScalar();
                dbConn.Close();

                return result;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/UsuarioDAO/Create:: " + e);
                dbConn.Close();
                return null;
            }
        }

        // Atualiza dados do usuário - Método de Admin que tbm altera o nível
        public static Boolean Update(Usuario usuario)
        {
            NpgsqlParameter param;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "UPDATE usuario SET nome = @nome, email = @email, prontuario = @prontuario, nivel = @nivel, senha=@senha" +
                    " WHERE id =@id  RETURNING id";

                param = new NpgsqlParameter("@nome", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
                param.Value = usuario.Nome;
                command.Parameters.Add(param);
                
                param = new NpgsqlParameter("@email", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
                param.Value = usuario.Email;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@prontuario", NpgsqlTypes.NpgsqlDbType.Varchar, 20);
                param.Value = usuario.Prontuario;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@nivel", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = usuario.Nivel;
                command.Parameters.Add(param);             

                param = new NpgsqlParameter("@senha", NpgsqlTypes.NpgsqlDbType.Varchar, 255);
                param.Value = usuario.Senha;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = usuario.ID;
                command.Parameters.Add(param);

                command.Prepare();

                var result = command.ExecuteScalar();
                dbConn.Close();               

                if (result != null && (int)result > 0)
                    return true;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/UsuarioDAO/Update2:: " + e);
                
                dbConn.Close();
            }
            return false;
        }

        // Atualiza dados do usuário - Método para o usuário alterar seus próprios dados
        // Não altera o nivel
        public static Boolean UpdateSelf(Usuario usuario)
        {
            NpgsqlParameter param;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "UPDATE Usuario(nome, email, prontuario, senha)"
                     + "values(@nome, @email, @prontuario) WHERE id = @id RETURNING id" ;

                param = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = usuario.ID;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@nome", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
                param.Value = usuario.Nome;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@email", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
                param.Value = usuario.Email;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@prontuario", NpgsqlTypes.NpgsqlDbType.Varchar, 20);
                param.Value = usuario.Prontuario;
                command.Parameters.Add(param);

                command.Prepare();
                var result = command.ExecuteScalar();
                dbConn.Close();

                if (result != null && (int)result > 0)
                    return true;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/UsuarioDAO/UpdateSelf:: " + e);
                dbConn.Close();
            }
            return false;
        }

        // Atualiza nivel de usuario
        public static Boolean UpdateNivel(int id, string novo_nivel)
        {
            NpgsqlParameter param;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "UPDATE Usuario(nivel) values(@nivel) WHERE id = @id return id";

                param = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = id;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@nivel", NpgsqlTypes.NpgsqlDbType.Varchar, 20);
                param.Value = novo_nivel;
                command.Parameters.Add(param);

                command.Prepare();

                var result = command.ExecuteScalar();
                dbConn.Close();

                if (result != null && (int)result > 0)
                    return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/UsuarioDAO/UpdateNivel:: " + e);
                dbConn.Close();
            }
            return false;
        }

        // TODO: HASH DE SENHAS
        // Atualiza senha
        public static Boolean UpdatePassword(int id, string senha)
        {
            NpgsqlParameter param;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "UPDATE Usuario(senha) values(@senha) WHERE id = @id";

                param = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = id;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@senha", NpgsqlTypes.NpgsqlDbType.Varchar, 255);
                param.Value = senha;
                command.Parameters.Add(param);

                command.Prepare();

                var result = command.ExecuteScalar();
                dbConn.Close();

                if (result != null && (int)result > 0)
                    return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/UsuarioDAO/UpdateNivel:: " + e);
                dbConn.Close();
            }
                return false;
            }

        //Exclui usuário
        public static Boolean Delete(int Id)
        {
            NpgsqlParameter param;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "DELETE FROM Usuario WHERE id = @id RETURNING 0";

                param = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = Id;
                command.Parameters.Add(param);

                var result = command.ExecuteScalar();

                dbConn.Close();
                if((int)result == 0)
                    return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/UsuarioDAO/Delete:: " + e);
                dbConn.Close();
            }
            return false;
        }

        // TODO, após implementar hash, necessário alterar aqui
        // Método de Login - Retorna objeto usuário
        // Campo pesquisa, pode ser pronturario ou email
        public static Usuario Login(string valor, string senha)
        {
            Usuario u = LoginViaEmail(valor, senha);
            if (u == null)
            {
                return LoginViaProntuario(valor, senha);
            }
            return u;
          
        }

        // Login pelo Email
        private static Usuario LoginViaEmail(string email, string senha)
        {
            NpgsqlParameter param;
            DataTable table = new DataTable();
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "SELECT * FROM Usuario WHERE email = @email AND senha = @senha";

                param = new NpgsqlParameter("@email", NpgsqlTypes.NpgsqlDbType.Varchar, 20);
                param.Value = email;
                command.Parameters.Add(param);

                param = new NpgsqlParameter("@senha", NpgsqlTypes.NpgsqlDbType.Varchar, 255);
                param.Value = senha;
                command.Parameters.Add(param);

                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);
                Usuario usuario;
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
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/UsuarioDAO/LoginViaEmail:: " + e);
            }
            dbConn.Close();
            return null;
        }

        // Login por Prontuario
        private static Usuario LoginViaProntuario(string prontuario, string senha)
        {
            NpgsqlParameter param;
            DataTable table = new DataTable();
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "SELECT * FROM Usuario WHERE prontuario = @prontuario AND senha = @senha";

                param = new NpgsqlParameter("@prontuario", NpgsqlTypes.NpgsqlDbType.Varchar, 20);
                param.Value = prontuario;
                command.Parameters.Add(param);
                
                param = new NpgsqlParameter("@senha", NpgsqlTypes.NpgsqlDbType.Varchar, 255);
                param.Value = senha;
                command.Parameters.Add(param);

                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);
                Usuario usuario;
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
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/UsuarioDAO/LoginViaProntuario:: " + e);
            }
            dbConn.Close();
            return null;
        }

        // Busca usuário por ID
        public static Usuario GetByID(int Id)
        {
            NpgsqlParameter param;
            DataTable table = new DataTable();
            Usuario usuario = null;
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "SELECT * FROM Usuario WHERE id = @id";

                param = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Integer, 0);
                param.Value = Id;
                command.Parameters.Add(param);

                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        usuario = new Usuario
                        {
                            ID = Convert.ToInt32(dr["id"]),
                            Nome = dr["nome"].ToString(),
                            Email = dr["email"].ToString(),
                            Prontuario = dr["prontuario"].ToString(),
                            Senha = dr["senha"].ToString(),
                            Nivel = Convert.ToInt32(dr["nivel"])
                        };
                        return usuario;
                    }
                        
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/UsuarioDAO/GetByID:: " + e);
            }
            dbConn.Close();
            return null;
        }

        // Lista todos usuários
        public static List<Usuario> GetAll()
        {
            DataTable table = new DataTable();
            List<Usuario> usuarios = new List<Usuario>();
            OpenConn();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(null, dbConn);
                command.CommandText = "SELECT * FROM usuario";
                NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command);
                Adpt.Fill(table);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        usuarios.Add(
                            new Usuario
                            {
                                ID = Convert.ToInt32(dr["id"]),
                                Nome = dr["nome"].ToString(),
                                Email = dr["email"].ToString(),
                                Prontuario = dr["prontuario"].ToString(),
                                Nivel = Convert.ToInt32(dr["nivel"]),
                                Niveis = GetTipos()
                                
                            }
                        );
                    }
                }
                dbConn.Close();
                return usuarios;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("INVENTARIO/UsuarioDAO/GetAll:: " + e);
                dbConn.Close();
                return null;
            }
            

        }

        // Retorna lista com tipos de usuários
        public static List<SelectListItem> GetTipos()
        {
            List<SelectListItem> lista_tipos = new List<SelectListItem>();
            int index = 0;
            foreach (var type in Enum.GetValues(typeof(TiposNivel)))
            {
                lista_tipos.Add(
                    new SelectListItem
                    {
                        Text = type.ToString(),
                        Value = index.ToString()
                    }
                );
                index++;
            }
            return lista_tipos;
        }
    }
}