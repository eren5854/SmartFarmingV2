using SmartFarmingV2.Entities.Models;
using System.Linq.Expressions;

namespace SmartFarmingV2.DataAccess.Repositories;
public interface IWeatherStationRepository
{
    void Create(WeatherStation weatherStation);
    void Update(WeatherStation weatherStation);
    void DeleteById(Guid id);
    IQueryable<WeatherStation> GetAll();
    WeatherStation? GetWeatherStationById(Guid weatherStationId);
    int GetNewProductCode();

    bool Any(Expression<Func<WeatherStation, bool>> predicate);
}
