using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Vinay.Models;

namespace Vinay.Data
{
    public class DataContextEF : DbContext
    {
        public DbSet<Computer>? Computer  {get; set; } // Computer is the class that represents the computer table in the database, and the DbSet is a collection of computer objects that we can query and manipulate using ef, this will also allow us to use LINQ queries to query the database and to perform CRUD operations on the computer table
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets<DataContextEF>()
                .Build();

            if(!options.IsConfigured)
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"), 
                options => options.EnableRetryOnFailure()); // callback function for enabling retry 
                // on failure, this is useful for transient faults like network issues or database timeouts, 
                // it will automatically retry the operation a few times before throwing an exception
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo"); // setting the default schema to dbo, 
            // this is optional but it is good practice to set it explicitly

            modelBuilder.Entity<Computer>().HasKey(c => c.ComputerId);
             // this will tell ef that the computer entity has a primary key called ComputerId, this is important for ef to track the changes of the entity and to generate the correct sql statements for insert, update and delete operations
            //.HasNoKey(); // this will tell ef that the computer entity does not have a primary key,;

            // modelBuilder.Entity<Computer>(entity =>
            // {
            //     entity.HasKey(e => e.Id); // primary key
            //     entity.Property(e => e.Price).HasColumnType("decimal(18,2)"); // setting the price column to be decimal with 18 digits and 2 decimal places
            //     entity.Property(e => e.Motherboard).IsRequired().HasMaxLength(100); // setting the motherboard column to be required and have a max length of 100 characters
            //     entity.Property(e => e.HasLTE).IsRequired(); // setting the HasLTE column to be required
            //     entity.Property(e => e.HasWifi).IsRequired(); // setting the HasWifi column to be required
            //     entity.Property(e => e.Videocard).IsRequired().HasMaxLength(100); // setting the Videocard column to be required and have a max length of 100 characters
            //     entity.Property(e => e.Releasedate).IsRequired(); // setting the Releasedate column to be required
            //     entity.Property(e => e.CPUcores).IsRequired(); // setting the CPUcores column to be required
            // });
        }
    }
}