using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seed;

public class Seeder(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
{
    public async Task SeedRole()
    {
        var newRoles = new List<IdentityRole>()
        {
            new(Role.SuperAdmin),
            new(Role.Admin),
            new(Role.User)
        };
        var existing = await roleManager.Roles.ToListAsync();
        foreach (var role in newRoles)
        {
            if (!existing.Exists(r => r.Name == role.Name))
            {
                await roleManager.CreateAsync(role);
            }
        }
    }

    public async Task SeedUser()
    {
        var existing = await userManager.FindByNameAsync("SuperAdmin");
        if (existing != null) return;
        var identity = new User()
        {
            UserName = "admin",
            PhoneNumber = "+992005442641",
            Email = "shinoyatzoda@gmail.com"
        };
        await userManager.CreateAsync(identity, "hello123");
        await userManager.AddToRoleAsync(identity, Role.SuperAdmin);
    }
}

public abstract class Role
{
    public const string SuperAdmin = "SuperAdmin";
    public const string Admin = "Admin";
    public const string User = "User";
}