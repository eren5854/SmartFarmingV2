namespace SmartFarmingV2.Entities.DTOs;

public sealed record CreateWeatherForecastLogDto(
    float WindSpeed,
    float WindDirection,
    float WaterLevel,
    float Temperature,
    float Humidity,
    float Pressure,
    float SunLight,
    float GroundHumidity,
    float ValfRelay);
