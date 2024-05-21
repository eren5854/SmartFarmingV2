using SmartFarmingV2.Entities.Abstractions;

namespace SmartFarmingV2.Entities.Models;

public sealed class Sensor : Entity
{
    public string SensorName { get; set; } = string.Empty;
    public float SensorData { get; set;} = 0;
    public string ProductCode { get; set;} = string.Empty;
    public Guid ProductTypeId { get; set;}
    public ProductType? ProductType { get; set;}
}
