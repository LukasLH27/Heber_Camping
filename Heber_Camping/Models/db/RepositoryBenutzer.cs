using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;


namespace Heber_Camping.Models.db
{
    public class RepositoryBenutzer : IRepositoryBenutzer
    {
        private string _connectionString = "Server=localhost;Database=db_auftrag;Uid=root;Pwd=formel1;";
        private MySqlConnection _connection;

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

        public void Close()
        {
            if ((this._connection != null) && (this._connection.State != ConnectionState.Closed))
            {
                this._connection.Close();
            }
        }

        public bool Insert(Benutzer benutzer)
        {

            if (benutzer == null)
            {
                return false;
            }

            DbCommand cmdInsert = this._connection.CreateCommand();

            cmdInsert.CommandText = "INSERT INTO users VALUES(null,@firstname,@lastname,@email,@userrole,sha2(@password, 256))";

            // Parameter erzeugt
            DbParameter paramFirstname = cmdInsert.CreateParameter();
            paramFirstname.ParameterName = "firstname";
            paramFirstname.Value = benutzer.Firstname;
            paramFirstname.DbType = DbType.String;

            DbParameter paramLastname = cmdInsert.CreateParameter();
            paramLastname.ParameterName = "lastname";
            paramLastname.Value = benutzer.Lastname;
            paramLastname.DbType = DbType.String;

            DbParameter paramEmail = cmdInsert.CreateParameter();
            paramEmail.ParameterName = "email";
            paramEmail.Value = benutzer.Email;
            paramEmail.DbType = DbType.String;

            DbParameter paramUserRole = cmdInsert.CreateParameter();
            paramUserRole.ParameterName = "userrole";
            paramUserRole.Value = benutzer.Rolle;
            paramUserRole.DbType = DbType.Int32;

            DbParameter paramPassword = cmdInsert.CreateParameter();
            paramPassword.ParameterName = "password";
            paramPassword.Value = benutzer.Password;
            paramPassword.DbType = DbType.String;


            

            cmdInsert.Parameters.Add(paramFirstname);
            cmdInsert.Parameters.Add(paramLastname);
            cmdInsert.Parameters.Add(paramEmail);
            cmdInsert.Parameters.Add(paramUserRole);
            cmdInsert.Parameters.Add(paramPassword);


            return cmdInsert.ExecuteNonQuery() == 1;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUserData(int id, Benutzer neueBenutzerDaten)
        {
            throw new NotImplementedException();
        }

        public Benutzer GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public Benutzer Login(BenutzerLogin user)
        {
            DbCommand cmdLogin = this._connection.CreateCommand();
            cmdLogin.CommandText = "SELECT * FROM users WHERE email=@email AND password =sha2(@password, 256)";

            DbParameter paramEmail = cmdLogin.CreateParameter();
            paramEmail.ParameterName = "email";
            paramEmail.Value = user.Email;
            paramEmail.DbType = DbType.String;

            DbParameter paramPassword= cmdLogin.CreateParameter();
            paramPassword.ParameterName = "password";
            paramPassword.Value = user.Password;
            paramPassword.DbType = DbType.String;

            cmdLogin.Parameters.Add(paramEmail);
            cmdLogin.Parameters.Add(paramPassword);

            using (DbDataReader reader = cmdLogin.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    return null;
                }

                reader.Read();
                return new Benutzer
                {
                    ID = Convert.ToInt32(reader["id"]),
                    Firstname = Convert.ToString(reader["firstname"]),
                    Lastname = Convert.ToString(reader["lastname"]),
                    Email = Convert.ToString(reader["email"]),
                    Rolle = (BenutzerRolle)Convert.ToInt32(reader["userRole"]),
                    Password = ""
                };
            }

        }
    }
}