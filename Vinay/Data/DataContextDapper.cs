using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Vinay.Data
{
    public class DataContextDapper
    {
        private readonly string _connectionstring;

        public DataContextDapper()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets<DataContextDapper>()
                .Build();

            _connectionstring = config.GetConnectionString("DefaultConnection");
        }
   
    public IEnumerable<T> LoadData<T>(string sql)
    {
        using IDbConnection dbConnection =
            new SqlConnection(_connectionstring);

        return dbConnection.Query<T>(sql);
    }

    public T LoadDataSignle<T>(string sql)
    {
        using IDbConnection dbConnection =
            new SqlConnection(_connectionstring);

        return dbConnection.QuerySingle<T>(sql);
    }

    public int ExecutesqlReturnCount(string sql, object parameters)
    {
        using IDbConnection dbConnection =
            new SqlConnection(_connectionstring);

        return dbConnection.Execute(sql, parameters);   // MUST pass parameters
    }

    public bool Executesql(string sql, object parameters)
    {
        using IDbConnection dbConnection =
            new SqlConnection(_connectionstring);

        return dbConnection.Execute(sql, parameters) > 0;  // MUST pass parameters
    }
   } 

}

