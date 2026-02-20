using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;


namespace Vinay.Data
{
   public class DataContextDapper
{
    private string _connectionstring =
        "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;User Id=SA;Password=Vinaysen321;";

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

