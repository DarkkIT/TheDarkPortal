﻿namespace TheDarkPortal.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using TheDarkPortal.Common;
    using TheDarkPortal.Data;
    using TheDarkPortal.Data.Models;
    using TheDarkPortal.Data.Seeding;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedUserAsync(userManager, "darkk334582@yahoo.com");
            await SeedUserAsync(userManager, "darkk33@yahoo.com");
            await SeedUserAsync(userManager, "darkk44@yahoo.com");
            await SeedUserAsync(userManager, "enemy001@thedarkportal.com");
            await SeedUserAsync(userManager, "enemy002@thedarkportal.com");
            await SeedUserAsync(userManager, "enemy003@thedarkportal.com");
            await SeedUserAsync(userManager, "enemy004@thedarkportal.com");
            await SeedUserAsync(userManager, "enemy005@thedarkportal.com");
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                var appUser = new ApplicationUser();
                appUser.UserName = username;
                appUser.Email = username;
                appUser.PhoneNumber = "0888999999";
                appUser.Silver = 500000;
                appUser.Gold = 500000;
                appUser.Platinum = 200000;
                IdentityResult result = new IdentityResult();

                if (username == "darkk334582@yahoo.com")
                {
                    result = userManager.CreateAsync(appUser, "darkk334582").Result;
                    appUser.PhoneNumber = "0888654321";
                    appUser.IsMob = false;
                }
                else if (username == "darkk33@yahoo.com")
                {
                    result = userManager.CreateAsync(appUser, "darkk33").Result;
                    appUser.PhoneNumber = "0888123456";
                    appUser.IsMob = false;
                }
                else if (username == "darkk44@yahoo.com")
                {
                    result = userManager.CreateAsync(appUser, "darkk44").Result;
                    appUser.PhoneNumber = "0888123456";
                    appUser.IsMob = false;
                }
                else if (username == "enemy001@thedarkportal.com")
                {
                    result = userManager.CreateAsync(appUser, "enemy001").Result;
                    appUser.PhoneNumber = "0888123456";
                    appUser.IsMob = true;
                }
                else if (username == "enemy002@thedarkportal.com")
                {
                    result = userManager.CreateAsync(appUser, "enemy002").Result;
                    appUser.PhoneNumber = "0888123456";
                    appUser.IsMob = true;
                }
                else if (username == "enemy003@thedarkportal.com")
                {
                    result = userManager.CreateAsync(appUser, "enemy003").Result;
                    appUser.PhoneNumber = "0888123456";
                    appUser.IsMob = true;
                }
                else if (username == "enemy004@thedarkportal.com")
                {
                    result = userManager.CreateAsync(appUser, "enemy004").Result;
                    appUser.PhoneNumber = "0888123456";
                    appUser.IsMob = true;
                }
                else if (username == "enemy005@thedarkportal.com")
                {
                    result = userManager.CreateAsync(appUser, "enemy005").Result;
                    appUser.PhoneNumber = "0888123456";
                    appUser.IsMob = true;
                }

                if (result.Succeeded)
                {
                    if (username == "darkk334582@yahoo.com")
                    {
                        userManager.AddToRoleAsync(appUser, GlobalConstants.AdministratorRoleName).Wait();
                    }
                }
            }
        }
    }
}
