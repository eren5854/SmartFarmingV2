namespace SmartFarmingV2.Entities.DTOs;
public sealed record UpdateSensorDto(
    Guid Id,
    string SensorName,
    float SensorData,
    string ProductCode,
    Guid ProductTypeId);
