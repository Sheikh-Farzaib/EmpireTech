using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helper
{
    public static class ExecuteQueryNonQuery
    {
       
        public static void ExecuteNonQuery(string query, SqlParameter[] parameters, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public static async Task<bool> ExecuteNonQueryAsync(string query, SqlParameter[] parameters, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    await connection.OpenAsync();

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    return rowsAffected > 0;
                }
            }
        }
        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }
        public static async Task<DataTable> ExecuteQueryAsync(string query, SqlParameter[] parameters, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync(); 

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);

                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();

                        await Task.Run(() => adapter.Fill(dataTable));

                        return dataTable;
                    }
                }
            }
        }
        public static void ExecuteNonQuery(string query, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public static async Task<DataTable> ExecuteQueryAsync(string query, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader); 
                        return dataTable;
                    }
                }
            }
        }
  


    }
}
