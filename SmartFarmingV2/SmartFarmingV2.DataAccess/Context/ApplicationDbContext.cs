using Microsoft.EntityFrameworkCore;
using SmartFarmingV2.Entities.Models;

namespace SmartFarmingV2.DataAccess.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<WeatherStation> WeatherStations { get; set; }
    public DbSet<Sensor> Sensors { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<WeatherForecastLog> WeatherForecastLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeatherStation>().HasQueryFilter(filter => !filter.IsDeleted);
        modelBuilder.Entity<ProductType>().HasQueryFilter(filter => !filter.IsDeleted);
        modelBuilder.Entity<Sensor>().HasQueryFilter(filter => !filter.IsDeleted);
        modelBuilder.Entity<WeatherForecastLog>().HasQueryFilter(filter => !filter.IsDeleted);
    }
}
