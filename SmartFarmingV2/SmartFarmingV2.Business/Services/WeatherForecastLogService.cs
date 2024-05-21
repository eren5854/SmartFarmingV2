using AutoMapper;
using SmartFarmingV2.DataAccess.Repositories;
using SmartFarmingV2.Entities.DTOs;
using SmartFarmingV2.Entities.Models;

namespace SmartFarmingV2.Business.Services;
public sealed class WeatherForecastLogService(
    WeatherForecastLogRepository weatherForecastLogRepository,
    IMapper mapper) : IWeatherForecastLogService
{
    public string Create(CreateWeatherForecastLogDto request)
    {
        WeatherForecastLog weatherForecastLog = mapper.Map<WeatherForecastLog>(request);
        weatherForecastLog.CreatedBy = "Admin";
        weatherForecastLog.CreatedDate = DateTime.Now;

        weatherForecastLogRepository.Create(weatherForecastLog);
        return DateTime.Now + " tarihinde kayıt oluşturldu.";
    }

    public List<WeatherForecastLog> GetAll()
    {
        List<WeatherForecastLog> weatherForecastLogs = weatherForecastLogRepository.GetAll().OrderBy(o => o.CreatedDate).ToList();
        return weatherForecastLogs;
    }

    public WeatherForecastLog? GetWeatherForecastLogById(Guid weatherForecastLogId)
    {
        WeatherForecastLog weatherForecastLogs = weatherForecastLogRepository.GetWeatherForecastLogById(weatherForecastLogId);
        return weatherForecastLogs;
    }
}
