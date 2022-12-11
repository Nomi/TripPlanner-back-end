using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;
using System.Reflection.Metadata;
using TripPlannerAPI.Models;

namespace TripPlannerAPI.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole { Name = "User", NormalizedName = "USER"},
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN"});
        }
    }
}
