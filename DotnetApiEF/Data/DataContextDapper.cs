using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace DotnetApiEF.Data
{
    public class DataContextDapper
    {
        // IConfiguration is used to get the connection string from the appsettings.json file and it is 
        // part of the ASP.NET Core framework
        private readonly IConfiguration _config;
        private readonly string? _connectionString;

        public DataContextDapper(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<T> LoadData<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Query<T>(sql);
        }

        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.QuerySingle<T>(sql);
        }

        public bool ExecuteSql( string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sql) > 0;
        }
         public int ExecuteSqlRowCount( string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sql);
        }
    
    public bool ExecuteSqlParameter( string sql, List<SqlParameter> parameters)
        {
            SqlCommand commandwithparam = new SqlCommand(sql);
            foreach(SqlParameter parameter in parameters)
            {
                commandwithparam.Parameters.Add(parameter);
            }
            SqlConnection dbConnection = new SqlConnection(_connectionString);

            dbConnection.Open();
            commandwithparam.Connection= dbConnection;

            int rowsaffected = commandwithparam.ExecuteNonQuery();
            
            dbConnection.Close();

            return rowsaffected>0;
        }
    }
}