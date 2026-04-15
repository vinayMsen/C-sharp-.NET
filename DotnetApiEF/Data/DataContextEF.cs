using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DotnetApiEF.Models;

namespace DotnetApiEF.Data{
public class DataContextEF : DbContext

{
    private readonly IConfiguration _config;

    public DataContextEF(IConfiguration config)
    {
        _config = config;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserJobInfo> UserJobInfo { get; set; }
    public DbSet<UserSalary> UserSalary { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                _config.GetConnectionString("DefaultConnection"),
                options => options.EnableRetryOnFailure()  // if fails then retry
            );
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");

        modelBuilder.Entity<User>()
            .ToTable("Users")
            .HasKey(u => u.UserId);

        modelBuilder.Entity<UserJobInfo>()
            .HasKey(u => u.UserId);

        modelBuilder.Entity<UserSalary>()
            .HasKey(u => u.UserId);
    }
  }
}