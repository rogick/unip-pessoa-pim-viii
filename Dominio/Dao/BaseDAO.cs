using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Pessoas.Dao
{
    public class BaseDAO : IDisposable
    {
        string connectionString = "Server=localhost\\SQLEXPRESS;Database=pim_viii;Trusted_Connection=True;Encrypt=False;"; 

         protected SqlConnection connection;

        // Métodos relativos a conexão e execução de queries

         protected SqlConnection getConnection() 
         {
             if (connection == null) 
             {
                 connection = new SqlConnection(connectionString);
             } 
             else if (connection.State == ConnectionState.Closed) 
             {
                 connection.Dispose();
                 connection = new SqlConnection(connectionString);
             }

             if (connection.State != ConnectionState.Open) 
                connection.Open();
             return connection;
         }

         protected void closeConnection() {
             if (connection != null && connection.State != ConnectionState.Closed) {
                connection.Close();
                connection.Dispose();
                connection = null;
             }
         }

         protected SqlCommand createSqlCommand(string sql, SqlTransaction transaction, Dictionary<string, object> parameters) 
         {
             var cmd = getConnection().CreateCommand();
             if (transaction != null)
                cmd.Transaction = transaction;
             cmd.CommandText = sql;
             
             if (parameters != null)
                foreach (string param in parameters.Keys)
                    cmd.Parameters.AddWithValue(param, parameters[param]);

            return cmd;
         }

         private int executeUpdateSql(string sql, SqlTransaction transaction, Dictionary<string, object> parameters)
         {
             using (var cmd = createSqlCommand(sql, transaction, parameters))
             {
                 return cmd.ExecuteNonQuery();
             };
         }

         private int executeInsertSql(string sql, SqlTransaction transaction, Dictionary<string, object> parameters)
         {
             sql += ";Select SCOPE_IDENTITY();";
             using (var cmd = createSqlCommand(sql, transaction, parameters))
             {
                 return Convert.ToInt32(cmd.ExecuteScalar());
             };
         }

        public IList<Dictionary<string, object>> executeSql(string sql) 
        {
            return executeSql(sql, null, null);
        }

        private IList<Dictionary<string, object>> executeSql(string sql, SqlTransaction transaction) 
        {
            return executeSql(sql, transaction, null);
        }

         private IList<Dictionary<string, object>> executeSql(string sql, SqlTransaction transaction, Dictionary<string, object> parameters)
         {
            using (var cmd = createSqlCommand(sql, transaction, parameters))
            {

                using (SqlDataReader reader = cmd.ExecuteReader()) 
                {
                    IList<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                    while(reader.Read()) {
                        Dictionary<string, object> linha = new Dictionary<string, object>();
                        for(int i = 0; i < reader.FieldCount; i++){
                            linha.Add(reader.GetName(i).ToUpper(), reader.GetValue(i));
                        } ;
                        list.Add(linha);
                    }

                    return list;
                }

            };
         }

    public void Dispose()
    {
      closeConnection();
    }
  }
}