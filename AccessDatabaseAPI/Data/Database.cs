using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;

namespace AccessDatabaseAPI.Data
{
    public class Database : IDisposable
    {
        private readonly MySqlConnection connection;

        public Database()
        {
            connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
            connection.Open();
        }

        public void CommandExecuter(string queryString)
        {
            var command = new MySqlCommand
            {
                CommandText = queryString,
                CommandType = CommandType.Text,
                Connection = connection
            };
            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MySqlDataReader CommandRetuner(string queryString)
        {
            var command = new MySqlCommand(queryString, connection);
            try
            {
                return command.ExecuteReader();
            }
            catch (MySqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
    }
}