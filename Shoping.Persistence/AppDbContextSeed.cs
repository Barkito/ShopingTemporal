
using Shoping.Domain;
using Shoping.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Shoping.Persistence;
public class AppDbContextSeed
{
    public static async Task SeedDataAsync(AppDbContext context)
    {
      

    }
    public static async Task SeedUsersAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        var testUser = await userManager.FindByNameAsync("administrator");
        if (testUser is null)
        {
            testUser = new User
            {
                UserName = "administrator"
            };

            await userManager.CreateAsync(testUser, "Passw0rd.1234");
            await userManager.CreateAsync(new User
            {
                UserName = "other_user"
            }, "Passw0rd.1234");
        }

        var adminRole = await roleManager.FindByNameAsync("Admin");
        if (adminRole is null)
        {
            await roleManager.CreateAsync(new IdentityRole
            {
                Name = "Admin"
            });

            await userManager.AddToRoleAsync(testUser, "Admin");
        }


    }



}




