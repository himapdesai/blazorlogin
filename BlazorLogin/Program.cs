
using BlazorLogin.Data;
using BlazorLogin.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<UserService>();
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

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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

var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
};
app.UseCookiePolicy(cookiePolicyOptions);

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
