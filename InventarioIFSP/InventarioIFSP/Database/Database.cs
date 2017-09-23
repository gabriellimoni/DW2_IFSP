using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioIFSP.Database
{
    public class Database
    {

        private static string strConn = @"Server=127.0.0.1;Port=5432;Database=inventario;User Id=postgres;Password=amoracha;";

        public static NpgsqlConnection Conexao
        {
            get
            {
                NpgsqlConnection conn =  new NpgsqlConnection(strConn);
                conn.Open();
                return conn;
            }
        }
    }
}