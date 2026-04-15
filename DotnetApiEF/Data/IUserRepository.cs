
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DotnetApiEF.Models;
using DotnetApiEF.Dto;

namespace DotnetApiEF.Data
{
    public interface IUserRepository
    {
        bool SaveChanges();
        void AddEntity<T>(T entityToAdd); // Ensure this is AddEntity
        void RemoveEntity<T>(T entityToDelete);
        IEnumerable<User> GetUsers();
        User GetSingleUser(int UserId);
        UserSalary GetSingleUserSalary(int UserId);
        UserJobInfo GetSingleUserJobInfo(int UserId);
    }
}