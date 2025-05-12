using Dapper;
using Microsoft.Data.SqlClient;

namespace TestFramework_NET.Common.Helpers
{
    // not checked
    internal class SqlHelper
    {
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
        public static int ExecuteNonQuery(string query, object? parameters = null)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var result = connection.Execute(query, parameters);
            connection.Close();

            return result;
        }

        /// <summary>
        /// Execute a SQL query that returns a single line result.
        /// </summary>
        /// <example> QuerySingle<User>("SELECT * FROM Users WHERE Id = @Id", new() { { "@Id", 1 } })</example>
        public static T? QuerySingle<T>(string query, object? parameters = null)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var result = connection.QueryFirstOrDefault<T>(query, parameters);
            connection.Close();

            return result; 
        }

        /// <summary>
        /// Execute a SQL query that returns a multi rows result.
        /// </summary>
        /// <example> QueryMany<User>("SELECT * FROM Users WHERE Age > @Age", new() { { "@Age", 18 } })</example>
        public static IEnumerable<T> QueryMany<T>(string query, object? parameters = null)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var result = connection.Query<T>(query, parameters);
            connection.Close();

            return result;
        }
    }
}
