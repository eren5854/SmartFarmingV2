using SmartFarmingV2.Entities.Abstractions;

namespace SmartFarmingV2.Entities.Models;

public sealed class ProductType: Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<WeatherStation>? weatherStations { get; set; }
    public List<Sensor>? sensors { get; set; }
}
