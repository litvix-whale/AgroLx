using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Core.Enums;

namespace Infrastructure.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedDatabase(AppDbContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            string adminPassword)
        {
            await SeedRoles(roleManager);

            await SeedUsers(userManager, adminPassword);
        }

        private static async Task SeedRoles(RoleManager<IdentityRole<Guid>> roleManager)
        {
            string[] roleNames = { "Admin", "Default" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
                }
            }
        }

        private static async Task SeedUsers(UserManager<User> userManager, string adminPassword)
        {
            // Seed Admin
            await SeedAdminUser(userManager, adminPassword);

            // Seed Regular Users
            var userSeedData = new List<(string username, string email)>
            {
                ("johndoe", "john@example.com"),
                ("janedoe", "jane@example.com"),
                ("bobsmith", "bob@example.com"),
                ("alicesmith", "alice@example.com")
            };

            foreach (var userData in userSeedData)
            {
                await SeedRegularUser(userManager, userData.username, userData.email);
            }
        }

        private static async Task SeedAdminUser(UserManager<User> userManager, string adminPassword)
        {
            var existingAdmin = await userManager.FindByEmailAsync("admin@example.com");

            if (existingAdmin == null)
            {
                var adminUser = new User
                {
                    UserName = "Admin",
                    Email = "admin@admin.com",
                    EmailConfirmed = true,
                    ProfilePicture = "pfp_6.png",
                    CreatedAt = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }

        private static async Task SeedRegularUser(UserManager<User> userManager, string username, string email)
        {
            var existingUser = await userManager.FindByEmailAsync(email);

            if (existingUser == null)
            {
                var user = new User
                {
                    UserName = username,
                    Email = email,
                    EmailConfirmed = true,
                    ProfilePicture = "pfp_5.png",
                    CreatedAt = DateTime.UtcNow,
                };

                var result = await userManager.CreateAsync(user, "User123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Default");
                }
            }
        }
    }
}