﻿using System;
using System.Linq;
using BasePackageModule1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BasePackageModule1.Data
{
    public static class SeedData
    {
        public static async void InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //await context.Database.EnsureDeletedAsync();

            //await context.Database.EnsureCreatedAsync();


            await context.SaveChangesAsync();


            if (!context.Users.Any())
            {

                ApplicationUser user = new ApplicationUser
                {
                    Email = "asif@admin.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "asif@admin.com",
                    Name = "Asif Shaikh"
                };


                var adminUser = await userManager.CreateAsync(user, "Asif@1234");

                if (adminUser.Succeeded)
                {

                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = "Admin"
                    });
                    await roleManager.CreateAsync(new IdentityRole
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
