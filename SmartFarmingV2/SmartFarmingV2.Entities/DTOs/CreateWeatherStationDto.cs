namespace SmartFarmingV2.Entities.DTOs;

public sealed record CreateWeatherStationDto(
    string WeatherStationName,
    float WindSpeed,
    float WindDirection,
    float WaterLevel,
    float Temperature,
    float Humidity,
    float Pressure,
    float SunLight,
    float Voltage,
    Guid ProductTypeId,
    string ProductCode);
