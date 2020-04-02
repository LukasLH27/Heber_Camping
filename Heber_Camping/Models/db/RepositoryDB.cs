using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;

namespace Heber_Camping.Models.db
{
    public class RepositoryDB : IRepositoryDB
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

        public bool Insert(Request request)
        {
 
            if (request == null)
            {
                return false;
            }

            DbCommand cmdInsert = this._connection.CreateCommand();

            cmdInsert.CommandText = "INSERT INTO requests VALUES(null,@firstname,@lastname,@email,@telnum,@dateArrival,@dateDeparture,@countOfPeople,@comments)";

            // Parameter erzeugt
            DbParameter paramFirstname = cmdInsert.CreateParameter();
            paramFirstname.ParameterName = "firstname";
            paramFirstname.Value = request.Firstname;
            paramFirstname.DbType = DbType.String;

            DbParameter paramLastname = cmdInsert.CreateParameter();
            paramLastname.ParameterName = "lastname";
            paramLastname.Value = request.Lastname;
            paramLastname.DbType = DbType.String;

            DbParameter paramEmail = cmdInsert.CreateParameter();
            paramEmail.ParameterName = "email";
            paramEmail.Value = request.Email;
            paramEmail.DbType = DbType.String;

            DbParameter paramTelNum = cmdInsert.CreateParameter();
            paramTelNum.ParameterName = "telnum";
            paramTelNum.Value = request.Telnum;
            paramTelNum.DbType = DbType.String;

            DbParameter paramDateArrival = cmdInsert.CreateParameter();
            paramDateArrival.ParameterName = "dateArrival";
            paramDateArrival.Value = request.DateArrival;
            paramDateArrival.DbType = DbType.Date;

            DbParameter paramDateDeparture = cmdInsert.CreateParameter();
            paramDateDeparture.ParameterName = "dateDeparture";
            paramDateDeparture.Value = request.DateDeparture;
            paramDateDeparture.DbType = DbType.Date;

            DbParameter paramCountPeople = cmdInsert.CreateParameter();
            paramCountPeople.ParameterName = "countOfPeople";
            paramCountPeople.Value = request.CountOfPeople;
            paramCountPeople.DbType = DbType.Int32;

            DbParameter paramComments = cmdInsert.CreateParameter();
            paramComments.ParameterName = "comments";
            paramComments.Value = request.Comments;
            paramComments.DbType = DbType.String;

            cmdInsert.Parameters.Add(paramFirstname);
            cmdInsert.Parameters.Add(paramLastname);
            cmdInsert.Parameters.Add(paramEmail);
            cmdInsert.Parameters.Add(paramTelNum);
            cmdInsert.Parameters.Add(paramDateArrival);
            cmdInsert.Parameters.Add(paramDateDeparture);
            cmdInsert.Parameters.Add(paramCountPeople);
            cmdInsert.Parameters.Add(paramComments);

            return cmdInsert.ExecuteNonQuery() == 1;
        }

    }
}