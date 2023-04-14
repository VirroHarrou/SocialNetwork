using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using SocialNetwork.Identity;
using SocialNetwork.Identity.Data;
using SocialNetwork.Identity.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuthDbContext>(options => options
                .UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

using (var scope = builder.Services.BuildServiceProvider())
{
    try
    {
        var context = scope
            .GetRequiredService<AuthDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = scope.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while app initialization");
    }
}



builder.Services.AddIdentity<AppUser, IdentityRole>(config =>
{
    config.Password.RequiredLength = 6;
    config.Password.RequireDigit = true;
    config.Password.RequireLowercase = false;
    config.Password.RequiredUniqueChars = 0;
    config.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<AppUser>()
    .AddInMemoryApiResources(Configuration.ApiResources)
    .AddInMemoryIdentityResources(Configuration.IdentityResources)
    .AddInMemoryApiScopes(Configuration.ApiScopes)
    .AddInMemoryClients(Configuration.Clients)
    .AddDeveloperSigningCredential();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "SocialNetwork.Identity.Cookie";
    config.LoginPath = "/Auth/Login";
    config.LogoutPath = "/Auth/Logout";
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(app.Environment.ContentRootPath, "Styles")),
    RequestPath = "/styles"
});

app.UseRouting();
app.UseIdentityServer();
app.MapDefaultControllerRoute();

app.Run();
