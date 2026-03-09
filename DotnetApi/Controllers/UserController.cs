using System.Data; 
using Dapper;
using Microsoft.Extensions.Configuration;
using DotnetApi.Data;

using Microsoft.AspNetCore.Mvc;
//alt+shift+f to format the code
using System.Runtime.InteropServices;



namespace DotnetApi.Controllers{

[ApiController] // send and recieve json data
[Route("[controller]")] // route to the controller
public class UserController : ControllerBase
{

    DataContextDapper _dapper;
    public UserController(IConfiguration config){

        //var connectionString = configu.GetConnectionString("DefaultConnection");
        // Use the connection string to connect to the database
        Console.WriteLine(config.GetConnectionString("DefaultConnection"));

        _dapper = new DataContextDapper(config);
    }

    // testing dapper 
    [HttpGet("TestConnection")]
    public DateTime TestConection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }

   
   [HttpGet("GetUsers/{testValue}")]
   //public IActionResult Test()

   public string[] GetUsers(string testValue)
    {
        
        string[] response = new string[]
        {
            "test1",
            "test2",
            "test3",
            testValue
        };
    return response;
    }

        // database connection test
    [HttpGet("GetUsers")]
    
    public IEnumerable<User> GetUsers()
        {
            string sql= @"select UserId,FirstName,LastName,Email,Gender,Active From Users";
            IEnumerable<User> users= _dapper.LoadData<User>(sql);
            return users;
        }

    [HttpGet("GetSingleUser/{UserId}")]
    public UserController GetSingleUser(int UserId)
        {
             
        }

 }
}


