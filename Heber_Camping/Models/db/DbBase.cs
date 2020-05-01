using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace Heber_Camping.Models.db
{
    public class DbBase : IDbBase
    {
        public string _connectionString = "Server=localhost;Database=db_auftrag;Uid=root;Pwd=formel1;";
        public MySqlConnection _connection = null;

        public void Close()
        {
            if ((this._connection != null) && (this._connection.State != ConnectionState.Closed))
            {
                this._connection.Close();
            }
        }
        public void Open()
        {
            if (this._connection == null)
            {
                this._connection = new MySqlConnection(this._connectionString);
            }
            if (this._connection.State != ConnectionState.Open)
            {
                this._connection.Open();
            }
        }
    }
}