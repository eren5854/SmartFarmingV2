using SmartFarmingV2.Entities.DTOs;
using SmartFarmingV2.Entities.Models;

namespace SmartFarmingV2.Business.Services;
public interface ISensorService
{
    string Create(CreateSensorDto request);
    string Update(UpdateSensorDto request);
    string DeleteById(Guid id);
    List<Sensor> GetAll();

}
