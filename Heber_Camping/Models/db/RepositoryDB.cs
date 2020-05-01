using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;

namespace Heber_Camping.Models.db
{
    public class RepositoryDB : DbBase, IRepositoryDB
    {
        
 
        public bool Insert(Request request)
        {

            if (request == null)
            {
                return false;
            }

            DbCommand cmdInsert = this._connection.CreateCommand();

            cmdInsert.CommandText = "INSERT INTO requests VALUES(null,@firstname,@lastname,@email,@telnum,@dateArrival,@dateDeparture,@countOfPeople,@comments,@RequestEdited)";

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

            DbParameter paramRequestEdited = cmdInsert.CreateParameter();
            paramRequestEdited.ParameterName = "requestEdited";
            paramRequestEdited.Value = false;
            paramRequestEdited.DbType = DbType.Boolean;

            cmdInsert.Parameters.Add(paramFirstname);
            cmdInsert.Parameters.Add(paramLastname);
            cmdInsert.Parameters.Add(paramEmail);
            cmdInsert.Parameters.Add(paramTelNum);
            cmdInsert.Parameters.Add(paramDateArrival);
            cmdInsert.Parameters.Add(paramDateDeparture);
            cmdInsert.Parameters.Add(paramCountPeople);
            cmdInsert.Parameters.Add(paramComments);
            cmdInsert.Parameters.Add(paramRequestEdited);

            return cmdInsert.ExecuteNonQuery() == 1;
        }

        public List<Request> GetRequests()
        {
            List<Request> requests = new List<Request>();

            DbCommand cmdGetAll = _connection.CreateCommand();
            cmdGetAll.CommandText = "select * from requests ORDER BY DateArrival ASC";

            using (DbDataReader reader = cmdGetAll.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    return null;
                }

                reader.Read();
                requests.Add(new Request
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Firstname = Convert.ToString(reader["firstname"]),
                    Lastname = Convert.ToString(reader["lastname"]),
                    Email = Convert.ToString(reader["email"]),
                    Telnum = Convert.ToString(reader["telnum"]),
                    DateArrival = Convert.ToDateTime(reader["dateArrival"]),
                    DateDeparture = Convert.ToDateTime(reader["dateDeparture"]),
                    CountOfPeople = Convert.ToInt32(reader["countOfPeople"]),
                    Comments = Convert.ToString(reader["comments"]),
                    RequestEdited = Convert.ToBoolean(reader["requestEdited"])
                });

            }

            

            return requests;

        }

        public bool Edit(int id)
        {
            DbCommand cmdSetEdit = _connection.CreateCommand();
            cmdSetEdit.CommandText = "UPDATE requests set RequestEdited = @Edit where id = @id ";

            DbParameter paramId = cmdSetEdit.CreateParameter();
            paramId.ParameterName = "id";
            paramId.Value = id;
            paramId.DbType = DbType.Int32;

            DbParameter paramEdit = cmdSetEdit.CreateParameter();
            paramEdit.ParameterName = "Edit";
            paramEdit.Value = true;
            paramEdit.DbType = DbType.Boolean;

            cmdSetEdit.Parameters.Add(paramId);
            cmdSetEdit.Parameters.Add(paramEdit);

            return cmdSetEdit.ExecuteNonQuery() == 1;

        }

        public bool Delete(int id)
        {
            DbCommand cmdDelete = _connection.CreateCommand();
            cmdDelete.CommandText = "DELETE from requests where id = @id ";

            DbParameter paramId = cmdDelete.CreateParameter();
            paramId.ParameterName = "id";
            paramId.Value = id;
            paramId.DbType = DbType.Int32;

            cmdDelete.Parameters.Add(paramId);

            return cmdDelete.ExecuteNonQuery() == 1;

        }


    }
}