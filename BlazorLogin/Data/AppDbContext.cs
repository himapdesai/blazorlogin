using Microsoft.EntityFrameworkCore;
using BlazorLogin.Data;
using ErrorBlazorLoginApp.Models;

namespace BlazorLogin.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }   

        public DbSet<Error> Errors { get; set; }     
    }

}