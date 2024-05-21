using SmartFarmingV2.DataAccess.Context;
using SmartFarmingV2.Entities.Models;
using System.Linq.Expressions;

namespace SmartFarmingV2.DataAccess.Repositories;
public sealed class SensorRepository(ApplicationDbContext context) : ISensorRepository
{
    public bool Any(Expression<Func<Sensor, bool>> predicate)
    {
        return context.Sensors.Any(predicate);
    }

    public void Create(Sensor sensor)
    {
        context.Add(sensor);
        context.SaveChanges();
    }

    public void DeleteById(Guid id)
    {
        Sensor sensor = GetSensorById(id);
        if (sensor is not null)
        {
            sensor.IsDeleted = true;
            context.SaveChanges();
        }
    }

    public IQueryable<Sensor> GetAll()
    {
        return context.Sensors.AsQueryable();
    }

    public Sensor? GetSensorById(Guid sensorId)
    {
        return context.Sensors.Where(p => p.Id == sensorId).FirstOrDefault();
    }

    public void Update(Sensor sensor)
    {
        context.Update(sensor);
        context.SaveChanges();
    }
}
