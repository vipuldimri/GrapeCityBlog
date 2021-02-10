using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using System.Linq;
using Entities.Constants;

namespace Entities.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedSuperAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser1 = new User
            {
                UserName = "AAAAA",
                Email = "A@gmail.com",
                FirstName = "vipul",
                LastName = "dimri",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (userManager.Users.All(u => u.Id != defaultUser1.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser1.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser1, "Helloworld_007");
                    await userManager.AddToRoleAsync(defaultUser1, Roles.Admin.ToString());
                }
            }


            var defaultUser2 = new User
            {
                UserName = "BBBBB",
                Email = "B@gmail.com",
                FirstName = "vipul",
                LastName = "dimri",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (userManager.Users.All(u => u.Id != defaultUser2.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser2.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser2, "Helloworld_007");
                    await userManager.AddToRoleAsync(defaultUser2, Roles.User.ToString());
                }
            }


            var defaultUser3 = new User
            {
                UserName = "CCCCC",
                Email = "C@gmail.com",
                FirstName = "vipul",
                LastName = "dimri",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (userManager.Users.All(u => u.Id != defaultUser3.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser3.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser3, "Helloworld_007");
                    await userManager.AddToRoleAsync(defaultUser3, Roles.User.ToString());
                }
            }

        }

    }

}
