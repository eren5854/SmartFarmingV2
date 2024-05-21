using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.SignalR;
using SmartFarmingV2.Business.Hubs;
using SmartFarmingV2.Business.Validator;
using SmartFarmingV2.DataAccess.Repositories;
using SmartFarmingV2.Entities.DTOs;
using SmartFarmingV2.Entities.Models;

namespace SmartFarmingV2.Business.Services;
public sealed class SensorService(
    ISensorRepository sensorRepository,
    IMapper mapper,
    IHubContext<SensorHub> hubContext) : ISensorService
{
    public string Create(CreateSensorDto request)
    {
        CreateSensorDtoValidator validator = new();
        ValidationResult result = validator.Validate(request);
        if (result.IsValid)
        {
            throw new ArgumentException(string.Join(", ", result.Errors.Select(s => s.ErrorMessage).ToList()));
        }

        bool isSensorNameExists = sensorRepository.Any(p => p.SensorName == request.SensorName);
        if (isSensorNameExists)
        {
            throw new ArgumentException("Bu sensor zaten mevcut");
        }

        Sensor sensor = mapper.Map<Sensor>(request);
        sensor.CreatedBy = "Admin";
        sensor.CreatedDate = DateTime.Now;

        sensorRepository.Create(sensor);
        return "Sensor kaydı oluşturuldu";
    }

    public string DeleteById(Guid id)
    {
        sensorRepository.DeleteById(id);
        return "Sensor kaydı silindi";
    }

    public List<Sensor> GetAll()
    {
        List<Sensor> sensors = sensorRepository.GetAll().OrderBy(p => p.SensorName).ToList();
        return sensors;
    }

    public string Update(UpdateSensorDto request)
    {
        UpdateSensorDtoValidator validator = new();
        ValidationResult result = validator.Validate(request);
        if (result.IsValid)
        {
            throw new ArgumentException(string.Join(", ", result.Errors.Select(s => s.ErrorMessage).ToList()));
        }

        Sensor? sensor = sensorRepository.GetSensorById(request.Id);
        if (sensor is null)
        {
            throw new ArgumentException("Sensor bilgisi bulunamadı");
        }

        if(sensor.SensorName != request.SensorName)
        {
            bool isSensorNameExists = sensorRepository.Any(p => p.SensorName == request.SensorName || p.ProductCode == request.ProductCode);
            if (isSensorNameExists)
            {
                throw new ArgumentException("Bu sensör zaten mevcut");
            }
        }

        mapper.Map(request, sensor);
        sensor.UpdatedDate = DateTime.Now;
        sensor.UpdatedBy = "Admin";
        hubContext.Clients.All.SendAsync("Sensor", sensor);
        sensorRepository.Update(sensor);
        return "Sensör bilgisi güncellendi";
    }
}
