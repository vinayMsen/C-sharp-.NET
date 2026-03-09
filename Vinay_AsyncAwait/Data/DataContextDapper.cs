using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Vinay.Models;

namespace Vinay.Data
{ 
    public class DataContextDapper
    {
      // this section is for the connection string to be stored in appsettings.json 
    // and accessed through user secrets, but for some reason i am not using this right now,
    //  so I have hardcoded the connection string for now, but I will try to fix it later

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
        
        //  private string _connectionstring =
        // "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;User Id=SA;Password=Vinaymiles321@;";
    public IEnumerable<T> LoadData<T>(string sql)
    {
        using IDbConnection dbConnection = new SqlConnection(_connectionstring);

        return dbConnection.Query<T>(sql);
    }

    public T LoadDataSingle<T>(string sql)
    {
        using IDbConnection dbConnection = new SqlConnection(_connectionstring);

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

