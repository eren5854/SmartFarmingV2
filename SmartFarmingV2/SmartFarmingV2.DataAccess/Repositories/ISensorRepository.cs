using SmartFarmingV2.Entities.Models;
using System.Linq.Expressions;

namespace SmartFarmingV2.DataAccess.Repositories;
public interface ISensorRepository
{
    void Create(Sensor sensor);
    void Update(Sensor sensor);
    void DeleteById(Guid id);
    IQueryable<Sensor> GetAll();
    Sensor? GetSensorById(Guid sensorId);
    bool Any(Expression<Func<Sensor, bool>> predicate);
}
