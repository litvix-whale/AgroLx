using DotNetEnv;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Logic.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Infrastructure.Repositories;
using Infrastructure.Seed;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    Env.Load();
}

// ������������ Google Maps API
builder.Configuration["GoogleMaps:ApiKey"] =
    Environment.GetEnvironmentVariable("GOOGLE_MAPS_API_KEY") ??
    builder.Configuration["GoogleMaps:ApiKey"];

builder.Services.AddControllersWithViews();

// Database ������������
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Environment.IsDevelopment()
        ? Environment.GetEnvironmentVariable("LOCAL_CONNECTION_STRING")
        : Environment.GetEnvironmentVariable("CONNECTION_STRING") ??
          builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseNpgsql(connectionString);

    // ³��������� sensitive logging � Production
    if (builder.Environment.IsProduction())
    {
        options.EnableSensitiveDataLogging(false);
    }
});

// Identity
builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// Cookie ������������
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;

    // Secure cookies � Production
    if (builder.Environment.IsProduction())
    {
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
    }
});

builder.Services.AddSignalR();

// Dependency Injection
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IGoogleMapsService, GoogleMapsService>();

builder.Services.AddHttpClient<IXmlDataService, XmlDataService>();
builder.Services.AddScoped<IXmlDataService, XmlDataService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Database �����������
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        var dbContext = services.GetRequiredService<AppDbContext>();

        // ̳������ ��
        if (app.Environment.IsProduction())
        {
            logger.LogInformation("Running database migrations...");
            await dbContext.Database.MigrateAsync();
        }
        else
        {
            dbContext.Database.Migrate();
        }

        // Seeding �����
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var xmlDataService = services.GetRequiredService<IXmlDataService>();
        var xmlSettings = services.GetRequiredService<IOptions<XmlDataSettings>>().Value;
        var adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD") ?? "DefaultAdmin123!";

        await DatabaseSeeder.SeedDatabase(
            dbContext,
            userManager,
            roleManager,
            xmlDataService,
            xmlSettings,
            adminPassword
        );

        logger.LogInformation("Database initialization completed successfully");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred during database initialization");
        throw; // Fail fast in case of critical errors
    }
}

// Configure pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}");

app.Run();