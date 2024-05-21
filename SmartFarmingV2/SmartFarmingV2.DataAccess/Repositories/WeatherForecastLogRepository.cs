using Microsoft.EntityFrameworkCore;
using SmartFarmingV2.Entities.Models;
using SmartFarmingV2.DataAccess.Context;
using System.Linq.Expressions;

namespace SmartFarmingV2.DataAccess.Repositories;
public sealed class WeatherForecastLogRepository(ApplicationDbContext context) : IWeatherForecastLogRepository
{
    public bool Any(Expression<Func<WeatherForecastLog, bool>> predicate)
    {
        return context.WeatherForecastLogs.Any(predicate);
    }

    public void Create(WeatherForecastLog weatherForecastLog)
    {
        context.Add(weatherForecastLog);
        context.SaveChanges();
    }

    public IQueryable<WeatherForecastLog> GetAll()
    {
        return context.WeatherForecastLogs.AsNoTracking().AsQueryable();
    }

    public WeatherForecastLog? GetWeatherForecastLogById(Guid weatherForecastLogId)
    {
        return context.WeatherForecastLogs.Find(weatherForecastLogId);
    }
}
