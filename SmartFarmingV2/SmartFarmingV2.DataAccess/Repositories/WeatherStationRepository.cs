using Microsoft.EntityFrameworkCore;
using SmartFarmingV2.DataAccess.Context;
using SmartFarmingV2.Entities.Models;
using System.Linq.Expressions;

namespace SmartFarmingV2.DataAccess.Repositories;
public sealed class WeatherStationRepository(ApplicationDbContext context) : IWeatherStationRepository
{
    public bool Any(Expression<Func<WeatherStation, bool>> predicate)
    {
        return context.WeatherStations.Any(predicate);
    }

    public void Create(WeatherStation weatherStation)
    {
        context.Add(weatherStation);
        context.SaveChanges();
    }

    public void DeleteById(Guid id)
    {
        WeatherStation? weatherStation = GetWeatherStationById(id);
        if (weatherStation is not null)
        {
            weatherStation.IsDeleted = true;
            context.SaveChanges();
        }
    }

    public IQueryable<WeatherStation> GetAll()
    {
        return context.WeatherStations.AsNoTracking().AsQueryable();
    }

    public int GetNewProductCode()
    {
        throw new NotImplementedException();
    }

    //public int GetNewProductCode()
    //{
    //    int lastProductCode = context.WeatherStations.Max(p => p.ProductCode);
    //    if(lastProductCode <= 100)
    //    {
    //        lastProductCode = 100;
    //    }
    //    lastProductCode++;
    //    return lastProductCode;
    //}

    public WeatherStation? GetWeatherStationById(Guid weatherStationId)
    {
        return context.WeatherStations.Find(weatherStationId);
    }

    public void Update(WeatherStation weatherStation)
    {
        context.Update(weatherStation);
        context.SaveChanges();
    }
}
