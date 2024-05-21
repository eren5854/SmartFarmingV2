using AutoMapper;
using SmartFarmingV2.Business.Services;
using SmartFarmingV2.DataAccess.Context;
using SmartFarmingV2.Entities.DTOs;
using SmartFarmingV2.Entities.Models;

namespace SmartFarmingV2.WebAPI.Works;

public class MachineLearningBackgroundService(
    ApplicationDbContext context, 
    ISensorService sensorService, 
    IWeatherStationService weatherStationService,
    IMapper mapper)
{
    public void Test()
    {
        Console.WriteLine("Hangfire Çalışıyor " + DateTime.Now);

        var x = sensorService.GetAll().Where(p => p.ProductCode == "SN1-GHS").FirstOrDefault();
        var y = sensorService.GetAll().Where(p => p.ProductCode == "SN1-VAL").FirstOrDefault();
        var z = weatherStationService.GetAll().Where(p => p.ProductCode == "SN1-WS1").FirstOrDefault();
        if ( z!.WaterLevel == 0)
        {
            UpdateSensorDto sensor = new(
                Id: y!.Id,
                SensorName: y!.SensorName,
                SensorData: 1,
                ProductCode: y.ProductCode,
                ProductTypeId: y.ProductTypeId
                );
            sensorService.Update(sensor);
        }
        else
        {
            UpdateSensorDto sensor = new(
                Id: y!.Id,
                SensorName: y!.SensorName,
                SensorData: 0,
                ProductCode: y.ProductCode,
                ProductTypeId: y.ProductTypeId
                );
            sensorService.Update(sensor);
        }
    }

    public void SaveDatabase()
    {
        var weatherStatus = weatherStationService.GetAll().FirstOrDefault();
        var groundHumidity = sensorService.GetAll().Where(p => p.ProductCode == "SN1-GHS").FirstOrDefault();
        var valfRelay = sensorService.GetAll().Where(p => p.ProductCode == "SN1-VAL").FirstOrDefault();
        CreateWeatherForecastLogDto weatherForecastLogDto = new(
            WindSpeed: weatherStatus.WindSpeed,
            WindDirection: weatherStatus.WindDirection,
            WaterLevel: weatherStatus.WaterLevel,
            Temperature: weatherStatus.Temperature,
            Humidity: weatherStatus.Humidity,
            Pressure: weatherStatus.Pressure,
            SunLight: weatherStatus.SunLight,
            GroundHumidity: groundHumidity.SensorData,
            ValfRelay: valfRelay.SensorData
            );
        WeatherForecastLog weatherForecastLog = mapper.Map<WeatherForecastLog>(weatherForecastLogDto);
        weatherForecastLog.CreatedBy = "Admin";
        weatherForecastLog.CreatedDate = DateTime.Now;
        context.Add( weatherForecastLog );
        context.SaveChanges();
    }


}
