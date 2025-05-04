using Microsoft.Data.SqlClient;

namespace TestFramework_NET.Common.Helpers
{
    // not checked
    internal class SqlHelper
    {
        /* Example of using params in Query
            int rows = SqlHelper.ExecuteNonQuery(
                "INSERT INTO Users (Name, Email) VALUES (@Name, @Email)",
                new()
                {
                    { "@Name", "Adam" },
                    { "@Email", "adam@example.com" }
                }
            );
        */
        internal static readonly string ConnectionString;

        static SqlHelper()
        {
            ConnectionString = TestContext.Parameters.Get("ConnectionString")
                ?? throw new ArgumentException("ConnectionString is null");
        }

        /// <summary>
        /// Execute a SQL query that does not return any results (e.g., INSERT, UPDATE, DELETE).
        /// </summary>
        /// <returns>Return number of affected rows</returns>
        public static int ExecuteNonQuery(string query, Dictionary<string, object>? parameters = null)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(query, connection);
            AddParameters(command, parameters);
            
            connection.Open();
          
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// Execute a SQL query that returns a single value (e.g., COUNT, SUM).
        /// </summary>
        /// <param name="map"> To map the result to a specific type.</param>
        /// r => new User { Id = r.GetInt32(0), Name = r.GetString(1) }
        /// r => new User { Id = r.GetInt32(r.GetOrdinal("Id")), Name = r.GetString(r.GetOrdinal("Name")) }
        public static T? QuerySingle<T>(string query, Func<SqlDataReader, T> map, Dictionary<string, object>? parameters = null)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(query, connection);
            AddParameters(command, parameters);

            connection.Open();
            using var reader = command.ExecuteReader();
            
            return reader.Read() ? map(reader) : default;
        }

        /// <summary>
        /// Execute a SQL query that returns a list of results.
        /// </summary>
        public static List<T> QueryList<T>(string query, Func<SqlDataReader, T> map, Dictionary<string, object>? parameters = null)
        {
            var results = new List<T>();
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(query, connection);
            AddParameters(command, parameters);

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                results.Add(map(reader));
            }

            return results;
        }

        private static void AddParameters(SqlCommand command, Dictionary<string, object>? parameters)
        {
            if (parameters == null) return;
            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
            }
        }
    }
}
