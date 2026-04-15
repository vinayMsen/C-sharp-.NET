using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DotnetApiEF.Data;
using DotnetApiEF.Models;
using DotnetApiEF.Dto;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
//alt+shift+f to format the code


namespace DotnetApiEF.Controllers
{

[ApiController] // send and recieve json data
[Route("[controller]")] // route to the controller
public class UserControllerEF : ControllerBase
{

    DataContextEF _entityFramework;  // DataContextEF instance 

    IUserRepository _userRepository;

    //auto mapper: is a dependecy injection that simplyfies the process of mapping one object type to another
    private readonly IMapper  _mapper;
    public UserControllerEF(IConfiguration config, IMapper mapper, IUserRepository userrepository){

        //var connectionString = configu.GetConnectionString("DefaultConnection");
        // Use the connection string to connect to the database
        Console.WriteLine(config.GetConnectionString("DefaultConnection"));

        _entityFramework = new DataContextEF(config);

        // _mapper = new Mapper(new MapperConfiguration(cfg =>
        // {
        //     cfg.CreateMap<UserDto,User>();
        // }));
        _mapper= mapper;

        _userRepository= userrepository; 
    }

    
    [HttpGet("TestConnection")]
    public DateTime TestConnection(){
    
        return DateTime.Now ;
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
                // Correctly call the method defined in your IUserRepository
                return _userRepository.GetUsers();
            }

    [HttpGet("GetSingleUser/{UserId}")]
    public User GetSingleUser(int UserId)
        {
            /*User user= _entityFramework.Users
            .Where(u=>u.UserId==UserId)
            .FirstOrDefault<User>(); // to return single value
            
            if(user!=null)
            {
                return user;
            }  

            throw new Exception("Failed to get user"); */
            return _userRepository.GetSingleUser(UserId);
        
        }



    [HttpPut("EditUser")]
    public IActionResult EditUser(User user) //taking input 
        {
             User userDb= _userRepository.GetSingleUser(user.UserId);
            // .Where(u=> u.UserId == user.UserId)
            // .FirstOrDefault<User>(); // to return single value
            
            if(userDb!=null)
            {
                 _mapper.Map(user, userDb); // automatic mapping above this manual mapping
              //  _userRepository.SaveChanges();

                if(_userRepository.SaveChanges())
                {
                    return Ok();
                }
                throw new Exception("Failed to update user");
            }   
            throw new Exception("Failed to Update user");


        }

    [HttpPost("AddUser")]
    public IActionResult AddEntity<User>(UserDto user) // to avoid sensitive data 
        {
            
                 User userDb = _mapper.Map<User>(user); // auto mapping

                /*userDb.Active= user.Active;
                userDb.FirstName=user.FirstName;
                userDb.LastName = user.LastName;
                userDb.Email = user.Email;
                userDb.Gender= user.Gender;
                */
            // adding user
            _userRepository.AddEntity<User>(userDb);
       // _userRepository.AddEnity<User>(userDb); // both are equivalent


                if(_userRepository.SaveChanges())
                {
                    return Ok();
                }

                throw new Exception("Failed to Add user");
        }


    [HttpDelete("DeleteSingleUser/{UserId}")]
    public IActionResult DeleteSingleUser(int UserId)
        {
            User userDb = _userRepository.GetSingleUser(UserId);
            // .Where(u=> u.UserId == UserId)
            // .FirstOrDefault<User>(); // check if user exist or not 
            
            if(userDb!=null)
            {
                _userRepository.RemoveEntity<User>(userDb);

                if(_userRepository.SaveChanges())
                {
                    return Ok();
                }
                throw new Exception("Failed to Delete user");
            }   
            throw new Exception("Failed to Delete user");

        }




   //UserSalary
    [HttpGet("UserSalary/{UserId}")]
    //IEnumerable<UserSalary> means:
    // A collection (list) of UserSalary objects
    public UserSalary GetUserSalaryEF(int UserId)
        {
            /*return _entityFramework.UserSalary
            .Where (u=> u.UserId== UserId)
            .ToList(); // IEnumerable a collection of list
            */
            return _userRepository.GetSingleUserSalary(UserId);
        }

    [HttpPost("UserSalary/{UserId}")]
    //UserSalary → the model class (mapped to your DB table)
    // UserForInsert → just a variable/parameter name JSON "{UserId: 1, UserSalary: 50000}" 
    public IActionResult PostUserSalaryEF(UserSalary UserForInsert)
        {
            _userRepository.AddEntity<UserSalary>(UserForInsert); // UserSalary is expected type from json
            if(_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Adding User salary Failed on Insert UserSalary");
        }

    
    [HttpDelete("UserSalary/{UserId}")]
           public IActionResult DeleteUserSalary( int UserId)
            {
                UserSalary userToDelete= _userRepository.GetSingleUserSalary(UserId); // _entityFramework.UserJobInfo
                // .Where(u=> u.UserId= UserId)
                // .FirstOrDefault<User>();


                if(userToDelete != null)
                {
                    _userRepository.RemoveEntity<UserSalary>(userToDelete);
                    if(_userRepository.SaveChanges())
                    {
                        return Ok();
                    }
                    
                    throw new Exception ("Deleting UserJobInfo Failed on save");
                }
                throw new Exception ("Deleting UserJobInfo Failed on save");
            }

    
    [HttpPut("UserSalary")]
    public IActionResult PutUserSalaryEF(UserSalary userforupdate)
        {
            UserSalary? usersalaryupdate= _userRepository.GetSingleUserSalary(userforupdate.UserId);
            // .Where(u=> u.UserId== userforupdate.UserId)
            // .FirstOrDefault();

            if(usersalaryupdate != null)
            {
                _mapper.Map(userforupdate, usersalaryupdate);
                if(_userRepository.SaveChanges())
                {
                    return Ok();
                }

                throw new Exception ("Updating User salary failed on Save");
            }
            throw new Exception ("Updating User salary failed on Save");
        }

    
    //UserJobInfo Table 

    [HttpGet("UserJobInfo/{UserId}")]
    public UserJobInfo GetJobInfoUserEF(int UserId)
        {
            // UserJobInfo? userjobinfo = _entityFramework.UserJobInfo
            // .Where(u=>u.UserId==UserId)
            // .FirstOrDefault<UserJobInfo>(); // to return single value
            
            // if(userjobinfo   !=null)
            // {
            //     return userjobinfo;
            // }  

            // throw new Exception("Failed to get user");
            return _userRepository.GetSingleUserJobInfo(UserId);
        
        }
    
    [HttpPut("UserJobInfo")]// Removed {UserId} from route since it's in the Body object
    public IActionResult PutUserJobInfoEF(UserJobInfo Userforupdate)
        {
           UserJobInfo userToUpdate = _userRepository.GetSingleUserJobInfo(Userforupdate.UserId);
            
            if(userToUpdate != null)
            {
                _mapper.Map(Userforupdate, userToUpdate); 

                if(_userRepository.SaveChanges()) 
                return Ok();

                throw new Exception ("Updating UserJobInfo Failed to update");
            }
            throw new Exception ("Updating UserJobInfo Failed to update");
        }
    [HttpDelete("UserJobInfo/{UserId}")]
        
            public IActionResult DeleteUserJobInfoEF( int UserId)
            {
                UserJobInfo? userToDelete = _userRepository.GetSingleUserJobInfo(UserId);


                if(userToDelete != null)
                {
                    _userRepository.RemoveEntity<UserJobInfo>(userToDelete);
                    if(_userRepository.SaveChanges())
                    {
                        return Ok();
                    }
                    
                    throw new Exception ("Deleting UserJobInfo Failed on save");
                }
                throw new Exception ("Deleting UserJobInfo Failed on save");
            }
        

 }
}


