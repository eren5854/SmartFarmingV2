using SmartFarmingV2.Entities.Models;
using System.Linq.Expressions;

namespace SmartFarmingV2.DataAccess.Repositories;
public interface IWeatherForecastLogRepository
{
    void Create(WeatherForecastLog weatherForecastLog);
    IQueryable<WeatherForecastLog> GetAll();
    WeatherForecastLog? GetWeatherForecastLogById(Guid weatherForecastLogId);
    bool Any(Expression<Func<WeatherForecastLog, bool>> predicate);

}
