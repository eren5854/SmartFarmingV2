using SmartFarmingV2.Entities.Abstractions;

namespace SmartFarmingV2.Entities.Models;

public sealed class WeatherStation : Entity
{
    public string WeatherStationName { get; set; } = string.Empty;
    public Guid ProductTypeId { get; set; }
    public ProductType? ProductType { get; set; }
    public string ProductCode { get; set; } = string.Empty;
    public float WindSpeed { get; set; } = 0;
    public float WindDirection {  get; set; } = 0;
    public float WaterLevel { get; set; } = 0;
    public float Pressure {  get; set; } = 0;
    public float Temperature { get; set; } = 0;
    public float Humidity { get; set; } = 0;
    public float SunLight { get; set; } = 0;
    public float Voltage { get; set; } = 0;
}
