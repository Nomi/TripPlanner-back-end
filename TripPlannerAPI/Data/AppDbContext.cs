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
        public virtual DbSet<TripTypePreference> TripTypesPreferences { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TripType>()
                .HasData(
                new TripType { Id = 1, TypeName = "car"},
                new TripType { Id = 2, TypeName = "bike"},
                new TripType { Id = 3, TypeName = "foot"});

            builder.Entity<PreferenceType>()
                .HasData(
                new PreferenceType { Id = 1, PreferenceTypeName = "Entertainment" },
                new PreferenceType { Id = 2, PreferenceTypeName = "Sightseeing" },
                new PreferenceType { Id = 3, PreferenceTypeName = "Exploring" },
                new PreferenceType { Id = 4, PreferenceTypeName = "Culture" },
                new PreferenceType { Id = 5, PreferenceTypeName = "History" },
                new PreferenceType { Id = 6, PreferenceTypeName = "Free ride" },
                new PreferenceType { Id = 7, PreferenceTypeName = "Training" },
                new PreferenceType { Id = 8, PreferenceTypeName = "Nature" });

            builder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole { Name = "User", NormalizedName = "USER"},
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN"});
        }
    }
}
