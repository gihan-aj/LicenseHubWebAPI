using LicenseHubWebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LicenseHubWebAPI.DataAccess.Context
{
    public class LicenseHubDbContext : DbContext
    {
        public LicenseHubDbContext(DbContextOptions<LicenseHubDbContext> options) : base(options) 
        {
            
        }

        public DbSet<User> Users { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>()
        //        .HasIndex(u => u.UserEmail)
        //        .IsUnique();
        //}
    }

}
