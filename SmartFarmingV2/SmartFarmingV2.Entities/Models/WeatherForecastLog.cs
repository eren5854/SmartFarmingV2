using SmartFarmingV2.Entities.Abstractions;

namespace SmartFarmingV2.Entities.Models;

public sealed class WeatherForecastLog : Entity
{
    public float? WindSpeed { get; set; }
    public float? WindDirection { get; set; }
    public float? WaterLevel { get; set; }
    public float? Temperature { get; set; }
    public float? Humidity { get; set; }
    public float? Pressure { get; set; }
    public float? SunLight { get; set; }
    public float? GroundHumidity { get; set; }
    public float? ValfRelay { get; set; }
}