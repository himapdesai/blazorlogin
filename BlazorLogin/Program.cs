
using BlazorLogin.Data;
using BlazorLogin.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ErrorBlazorLoginApp.Services;
using ErrorBlazorLoginApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ErrorService>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromSeconds(30);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden";
        options.LoginPath = "/login";

    });


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var db = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.WeatherForecasts.Any())
    {
        db.WeatherForecasts.AddRange(
            new WeatherForecast { Date = DateTime.Now.AddDays(1), TemperatureC = 20, Summary = "Mild" },
            new WeatherForecast { Date = DateTime.Now.AddDays(2), TemperatureC = 25, Summary = "Warm" },
            new WeatherForecast { Date = DateTime.Now.AddDays(3), TemperatureC = 10, Summary = "Cool" }
        );
        db.SaveChanges();
    }
}
using (var serviceScope = app.Services.CreateScope())
{
    var db = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!db.Errors.Any())
    {
        db.Errors.AddRange(
            new Error { Title = "STCON", Date = "2025/10/06", Description = "Retry error with Id XXX" },
            new Error { Title = "DC", Date = "2025/10/09", Description = "DC: 5426216.2 weight do not match" },
            new Error { Title = "OVDC", Date = "2025/10/10", Description = "OVDC error for Kem" }
        );
        db.SaveChanges();
    }
}
    var cookiePolicyOptions = new CookiePolicyOptions
    {
        MinimumSameSitePolicy = SameSiteMode.Strict,
    };

//var builder1 = WebApplication.CreateBuilder(args);

//var app1 = builder.Build();

app.UseCookiePolicy(cookiePolicyOptions);

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

internal class BlazorLoginAppErrorContext
{
}