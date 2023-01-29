//using System.Diagnostics;
//using System;
//using System.Linq;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using TripPlannerAPI.Models;
//using MyTested.AspNetCore.Mvc.Utilities.Extensions;
//using Microsoft.AspNetCore.Identity;

//namespace TripPlannerAPI.Data
//{
//    public static class DbInitializer
//    {
//        /// <summary>
//        /// The following methods is just a stopgap solution for seeding the database with admin role if it doesn't exist.
//        /// Clearly, a lot of things need to change for Security (e.g. the password and username will be visible publicly in this file, without encryption),
//        /// and refactoring.
//        /// </summary>
//        /// <param name="_appDbContext">The Database Context.</param>
//        /// <param name="_userManager">The service/repository for user DB operations/interactions.</param>
//        /// <exception cref="Exception">Thrown in case of the following errors: 1.) Database doesn't exist. </exception>
//        public async static void Initialize(AppDbContext _appDbContext, UserManager<User> _userManager)
//        {
//            //If DB not created, throw exception:
//            if (!_appDbContext.Database.EnsureCreated()) 
//            {
//                throw new Exception("Database doesn't exist.");
//            }

//            //Check if the initialization is needed:
//            var adminAt101 = _appDbContext.Users.First(u => (u.UserName == "Admin@101")); ///TODO: need to remove it being hardcoded.
//            var adminRole = _appDbContext.Roles.First(r => (r.Name=="admin")); //The role should already be there (refer to AppDbContext class).
//            if (adminAt101!=null && _appDbContext.UserRoles.Any(x => ((x.UserId == adminAt101.Id) && (x.RoleId == adminRole.Id)))) ///TODO: need to remove it being hardcoded.
//            {
//                return;   // DB has already been seeded
//            }
//            if (null == adminAt101)
//            {
//                adminAt101 = new User { UserName = "Admin@101", Email = "admin@admin.admin" };

//                var result = await _userManager.CreateAsync(adminAt101, "Admin@101"); ///TODO: need to remove it being hardcoded, and to make the password secret.
//            }
//            var roleResult = await _userManager.AddToRoleAsync(adminAt101, adminRole.Name);
//            _appDbContext.SaveChanges();
//        }
//    }
//}
