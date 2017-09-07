using System;
using System.Collections;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace ReadyCollect.Data
{
    internal class DataHelper<T>
    {
        string conString;

        internal DataHelper(string connectionstring)
        {
            conString = connectionstring;
        }

        IDbConnection Connection
        {
            get
            {
                return new SqlConnection(conString);
            }
        }

        internal List<T> ExecProcedureWithData(string spName, DynamicParameters parameters = null)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var data = dbConnection.Query<T>(spName, parameters, commandType: CommandType.StoredProcedure);
                    return data.AsList<T>();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal void ExecProcedure(string spName, out int retValue, DynamicParameters parameters = null)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    retValue = dbConnection.Execute(spName, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal List<T> SelectData(string query)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    return dbConnection.Query<T>(query).AsList<T>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void ManageData(string query, out int status)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    status = dbConnection.Execute(query);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
