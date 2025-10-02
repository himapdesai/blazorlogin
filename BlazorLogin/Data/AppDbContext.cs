using Microsoft.EntityFrameworkCore;
using BlazorLogin.Data;

namespace BlazorLogin.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }        
    }

}