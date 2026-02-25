using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Vinay_Files.Models;

namespace Vinay_Files.Data
{
        public class DataContextEF: DbContext
    {
        private readonly string? _connectionstring;
        
        public DataContextEF(IConfiguration config)
        {
            _connectionstring = config.GetConnectionString("DefaultConnection");
        }
        
         public DbSet<Computer>? Computer { get; set; } 
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(_connectionstring,
                    options => options.EnableRetryOnFailure());
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo"); // setting the default schema to dbo, 
            // this is optional but it is good practice to set it explicitly

            // To explicitly map the table : modelBuilder.Entity<Computer>()
         // .ToTable("Computer"); 
         
            modelBuilder.Entity<Computer>().HasKey(c => c.ComputerId);

        }
    }
        
}