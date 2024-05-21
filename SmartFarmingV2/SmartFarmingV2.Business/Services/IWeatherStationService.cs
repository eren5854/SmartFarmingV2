using SmartFarmingV2.Entities.DTOs;
//using SmartFarmingV2.Entities.DTOs;
using SmartFarmingV2.Entities.Models;

namespace SmartFarmingV2.Business.Services;
public interface IWeatherStationService
{
    string Create(CreateWeatherStationDto request);
    string Update(UpdateWeatherStationDto request);
    string DeleteById(Guid id);
    List<WeatherStation> GetAll();
}
