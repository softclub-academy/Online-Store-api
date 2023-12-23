using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seed;

public class Seeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
    ApplicationContext context)
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
        var existing = await userManager.FindByNameAsync("admin");
        if (existing != null) return;
        var identity = new ApplicationUser()
        {
            UserName = "admin",
            PhoneNumber = "+992005442641",
            Email = "shinoyatzoda@gmail.com"
        };
        await userManager.CreateAsync(identity, "hello123");
        await userManager.AddToRoleAsync(identity, Role.SuperAdmin);
        var userProfile = new UserProfile()
        {
            ApplicationUserId = identity.Id,
            FirstName = "",
            LastName = "",
            Email = "",
            PhoneNumber = "",
            Image = ""
        };
        await context.UserProfiles.AddAsync(userProfile);
        await context.SaveChangesAsync();
    }

    public async Task SeedCatalog()
    {
        var i = 0;
        foreach (var catalog in new SeedCatalog().Catalogs)
        {
            var newCategory = new Category()
            {
                CategoryName = catalog,
                CategoryImage = ""
            };
            var exist = await context.Categories.AnyAsync(c => c.CategoryName == catalog);
            if (exist)
            {
                i++;
                continue;
            }
            await context.Categories.AddAsync(newCategory);
            await context.SaveChangesAsync();
            foreach (var category in new SeedCatalog().Category[i])
            {
                var newSubCategory = new SubCategory()
                {
                    CategoryId = newCategory.Id,
                    SubCategoryName = category
                };
                await context.SubCategories.AddAsync(newSubCategory);
                await context.SaveChangesAsync();
            }

            i++;
        }
    }
}

public abstract class Role
{
    public const string SuperAdmin = "SuperAdmin";
    public const string Admin = "Admin";
    public const string User = "User";
}