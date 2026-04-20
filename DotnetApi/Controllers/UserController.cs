using System.Data; 
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DotnetApi.Data;
using DotnetApi.Models.User;
using DotnetApi.Dto;
using Microsoft.AspNetCore.Mvc;
//alt+shift+f to format the code




namespace DotnetApi.Controllers
{

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
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }

   
   [HttpGet("GetValue/{testValue}")]
   //public IActionResult Test()

   public string[] GetValue(string testValue)
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
            string sql= @"select UserId,FirstName,LastName,
            Email,Gender,Active 
            From Users";
            IEnumerable<User> users= _dapper.LoadData<User>(sql);
            return users;
        }

    [HttpGet("GetSingleUser/{UserId}")]
    public User GetSingleUser(int UserId)
        {
            string sql= @"select UserId,FirstName,LastName,
            Email,Gender,Active 
            From Users 
            where UserId=  " + UserId.ToString(); // INT user input to string since query  is string type
            User signleuser= _dapper.LoadDataSingle<User>(sql);
            return signleuser;
        }



    [HttpPut("EditUser")]
    public IActionResult EditUser(User user) //taking input 
        {
        string sql = @"Update Users Set 
                Email = '" + user.Email + 
                "', LastName = '" + user.LastName +
                "' WHERE UserId = " + user.UserId;
            if(_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            
            throw new Exception("Failed to Update User");
        }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserDto user) // to avoid sensitive data 
        {
            
            string sql =@"INSERT INTO Users (FirstName, LastName, Email, Gender, Active) 
            VALUES ("+
            "'"    + user.FirstName+
            "', '" + user.LastName+
            "', '" + user.Email+
            "', '" + user.Gender+
            "', '" + user.Active+
            "')";

            Console.WriteLine(sql);
            if(_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            
            throw new Exception("Failed to Add User");
        }


    [HttpDelete("DeleteSingleUser/{UserId}")]
    public IActionResult DeleteSingleUser(int UserId)
        {
            string sql = @"Delete from Users
             where UserId= "+ UserId.ToString();

             Console.WriteLine(sql);
            if(_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            
            throw new Exception("Failed to Delete User");

        }

 }
}


