
/*Act as a middle layer

Hide direct interaction with Entity Framework (DbContext)
Provide reusable DB operations
*/
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DotnetApiEF.Models;
using DotnetApiEF.Data;
using DotnetApiEF.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DotnetApiEF.Data
{
    public class UserRepository : IUserRepository   
    {
        DataContextEF  _entityFramework;
        
        public UserRepository(IConfiguration config)
        {
            _entityFramework = new DataContextEF(config);
        }

        public bool SaveChanges()
        {
            return _entityFramework.SaveChanges()>0;
        }

       
       /*AddEnity()  → mark for insert  
        SaveChanges() → actually insert into DB

        AddEntity() → put item in cart 🛒
        RemoveEntity() → remove item from cart
        SaveChanges() → checkout (actually commit)
*/

        // public bool AddEnity<T> (T entityToAdd)
        public void AddEntity<T> (T entityToAdd)
        {
            if(entityToAdd != null)
            {
                _entityFramework.Add(entityToAdd);
               // return true;
            }
            //return false;
        }
        
        public void RemoveEntity<T> (T entityToDelete)
        {
            if(entityToDelete != null)
            {
                _entityFramework.Remove(entityToDelete);
               
            }
        }

        // doing what abstraction actually do, removing every instance of EntityFramework from EF controller
         public IEnumerable<User> GetUsers()
        {
           return _entityFramework.Users.ToList();
        }

        public User GetSingleUser(int UserId)
        {
            User user= _entityFramework.Users
            .Where(u=>u.UserId==UserId)
            .FirstOrDefault<User>(); // to return single value
            
            if(user!=null)
            {
                return user;
            }  

            throw new Exception("Failed to get user");
        
        }


        //salary
         public UserSalary GetSingleUserSalary(int UserId)
        {
            UserSalary? usersalary = _entityFramework.UserSalary
            .Where(u=>u.UserId==UserId)
            .FirstOrDefault<UserSalary>(); // to return single value
            
            if(usersalary != null)
            {
                return usersalary;
            }  

            throw new Exception("Failed to get user");
        
        }  


        //jobinfo
        public UserJobInfo GetSingleUserJobInfo(int UserId)
        {
            UserJobInfo? userjobinfo = _entityFramework.UserJobInfo
            .Where(u=>u.UserId==UserId)
            .FirstOrDefault<UserJobInfo>(); // to return single value
            
            if(userjobinfo   !=null)
            {
                return userjobinfo;
            }  

            throw new Exception("Failed to get user");
        
        }        
      

    }
}