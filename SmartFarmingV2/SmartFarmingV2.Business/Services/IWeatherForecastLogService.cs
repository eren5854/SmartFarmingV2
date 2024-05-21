using SmartFarmingV2.Entities.DTOs;
using SmartFarmingV2.Entities.Models;

namespace SmartFarmingV2.Business.Services;
public interface IWeatherForecastLogService
{
    string Create(CreateWeatherForecastLogDto request);
    List<WeatherForecastLog> GetAll();
    WeatherForecastLog? GetWeatherForecastLogById(Guid weatherForecastLogId);
}
