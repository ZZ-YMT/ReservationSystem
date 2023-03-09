using Application.Enums;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Seeds
{
    public static class DefaultAdmin
    {
        //DODAĆ IMPLEMENTACJE
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "sa",
                Email = "sa@wms.com",
                FirstName = "Administrator",
                LastName = "Administrator",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Fzbw@JVytU@Z");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Doctor.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());                    
                }

            }
        }
    }
}
