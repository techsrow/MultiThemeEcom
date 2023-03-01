using System;
using System.Linq;
using BasePackageModule2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BasePackageModule2.Data
{
    public static class SeedData
    {
        public static async void InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

          //   await context.Database.EnsureDeletedAsync();

             await context.Database.EnsureCreatedAsync();


             await context.SaveChangesAsync();


            
            if (!context.Users.Any())
            {

                ApplicationUser user = new ApplicationUser
                {
                    FirstName = "Asif",
                    LastName = "Shaikh",
                    Email = "admin@email.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "admin@email.com",
                    PhoneNumber = "+918808188031"
                };


                var adminUser = await userManager.CreateAsync(user, "1234");

                if (adminUser.Succeeded)
                {

                    await roleManager.CreateAsync(new IdentityRole()
                    {
                        Name = "Admin"
                    });
                    await roleManager.CreateAsync(new IdentityRole()
                    {
                        Name = "User"
                    });

                    await userManager.AddToRoleAsync(user, "Admin");
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
