namespace SmartFarmingV2.Entities.DTOs;
public sealed record CreateSensorDto(
    string SensorName,
    float SensorData,
    string ProductCode,
    Guid ProductTypeId);
