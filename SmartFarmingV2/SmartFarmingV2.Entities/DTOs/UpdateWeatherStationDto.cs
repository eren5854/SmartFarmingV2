namespace SmartFarmingV2.Entities.DTOs;
public sealed record UpdateWeatherStationDto(
    Guid Id,
    string WeatherStationName,
    float WindSpeed,
    float WindDirection,
    float WaterLevel,
    float Temperature,
    float Humidity,
    float Pressure,
    float SunLight,
    float Voltage,
    string ProductCode,
    Guid ProductTypeId);
