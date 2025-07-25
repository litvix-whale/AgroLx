using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enums;
using Core.Interfaces;
using Infrastructure.Xml.Configs;
using Infrastructure.Xml.Interfaces;


namespace Infrastructure.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedDatabase(AppDbContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IXmlDataService xmlDataService,
            XmlDataSettings xmlSettings,
            string adminPassword)
        {
            await SeedRoles(roleManager);

            await SeedUsers(userManager, adminPassword);

            await SeedRealEstatesFromXml(context, userManager, xmlDataService, xmlSettings);
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

        private static async Task SeedRealEstatesFromXml(AppDbContext context,
            UserManager<User> userManager,
            IXmlDataService xmlDataService,
            XmlDataSettings xmlSettings)
        {
            if (context.RealEstates.Any())
            {
                Console.WriteLine("Real estate data already exists, skipping seeding");
                return;
            }

            var user = await userManager.FindByEmailAsync("john@example.com");
            if (user == null)
            {
                Console.WriteLine("Default user not found, cannot seed real estate data");
                return;
            }

            if (!xmlSettings.EnableXmlSeeding)
            {
                Console.WriteLine("XML seeding is disabled, using static data");
                await SeedStaticRealEstates(context, user);
                return;
            }

            try
            {
                Console.WriteLine("Starting XML data seeding with settings:");
                Console.WriteLine($"  - Max items: {xmlSettings.MaxItemsToLoad}");
                Console.WriteLine($"  - Batch size: {xmlSettings.BatchSize}");
                Console.WriteLine($"  - Load from end: {xmlSettings.LoadFromEnd}");
                Console.WriteLine($"  - Feed URL: {xmlSettings.FeedUrl}");

                await xmlDataService.SeedRealEstateInBatchesAsync(context, user.Id);

                var totalCount = await context.RealEstates.CountAsync();
                Console.WriteLine($"XML seeding completed. Total real estate items in database: {totalCount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during XML seeding: {ex.Message}");

                if (xmlSettings.FallbackToStaticData)
                {
                    Console.WriteLine("Falling back to static seed data...");
                    await SeedStaticRealEstates(context, user);
                }
            }
        }

        private static async Task SeedStaticRealEstates(AppDbContext context, User user)
        {

            var estates = new List<RealEstate>
    {
        new RealEstate
        {
            Title = "������ 1 ������� �������� �� ����������",
            Description = "�������� � ��������, ������ �������, ��������, ����.",
            IsNewBuilding = true,
            Category = ProductCategoryEnum.Residential,
            RealtyType = RealEstateTypeEnum.Apartment,
            Deal = DealTypeEnum.Rent,
            Country = "������",
            Region = "�������� �������",
            Locality = "����",
            Borough = "����������",
            Street = "����������",
            StreetType = "������",
            Latitude = 49.8428706,
            Longitude = 23.9994757,
            Floor = 3,
            TotalFloors = 7,
            AreaTotal = 68.7f,
            AreaLiving = 20f,
            AreaKitchen = 38.02f,
            RoomCount = 1,
            NewBuildingName = "�� Manhattan",
            Price = 600,
            Currency = CurrencyEnum.USD,
            Images = new List<ProductImage>
            {
                new() { Url = "https://crm-08498194.s3.eu-west-1.amazonaws.com/zahid-rent/estate-images/eef2b874cce8e541ba533a107b08affb.jpg", UiPriority = 0 },
                new() { Url = "https://crm-08498194.s3.eu-west-1.amazonaws.com/zahid-rent/estate-images/eef2b874cce8e541ba533a107b08affb.jpg", UiPriority = 1 }
            },
            UserId = user.Id
        },
        new RealEstate
        {
            Title = "������ 3-��. ��������. ���. ����������-����� �����������",
            Description = "������ 3-��. ��������. ���. ����������-����� �����������. ������ �����������. ʳ����� ����������. ���� �� �������� �������. ��� �������. ��� ��������� �������. �����. ��������. ³��������.",
            IsNewBuilding = false,
            Category = ProductCategoryEnum.Residential,
            RealtyType = RealEstateTypeEnum.Apartment,
            Deal = DealTypeEnum.Rent,
            Country = "������",
            Region = "�������� �������",
            Locality = "����",
            Borough = "�����������",
            Street = "����������� �����",
            StreetType = "������",
            Latitude = 49.8375986,
            Longitude = 24.0764789,
            Floor = 4,
            TotalFloors = 9,
            AreaTotal = 70,
            AreaKitchen = 11,
            RoomCount = 3,
            Price = 13500,
            Currency = CurrencyEnum.UAH,
            Images = new List<ProductImage>
            {
                new() { Url = "https://crm-08498194.s3.eu-west-1.amazonaws.com/zahid-rent/estate-images/eef2b874cce8e541ba533a107b08affb.jpg", UiPriority = 0 },
                new() { Url = "https://crm-08498194.s3.eu-west-1.amazonaws.com/zahid-rent/estate-images/eef2b874cce8e541ba533a107b08affb.jpg", UiPriority = 1 },
                new() { Url = "https://crm-08498194.s3.eu-west-1.amazonaws.com/zahid-rent/estate-images/eef2b874cce8e541ba533a107b08affb.jpg", UiPriority = 2 }
            },
            UserId = user.Id
        }
    };

            await context.RealEstates.AddRangeAsync(estates);
            await context.SaveChangesAsync();
        }

    }
}