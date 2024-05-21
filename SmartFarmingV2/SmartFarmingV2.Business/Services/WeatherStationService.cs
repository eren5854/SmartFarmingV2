using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.SignalR;
using SmartFarmingV2.Business.Hubs;
using SmartFarmingV2.Business.Validator;
using SmartFarmingV2.DataAccess.Repositories;
using SmartFarmingV2.Entities.DTOs;
using SmartFarmingV2.Entities.Models;

namespace SmartFarmingV2.Business.Services;
public sealed class WeatherStationService(
    IWeatherStationRepository weatherStationRepository, 
    IMapper mapper,
    IHubContext<WeatherStationHub> hubContext) : IWeatherStationService
{
    public string Create(CreateWeatherStationDto request)
    {
        CreateWeatherStationDtoValidator validator = new();
        ValidationResult result = validator.Validate(request);
        if(!result.IsValid)
        {
            throw new ArgumentException(string.Join(", ", result.Errors.Select(s => s.ErrorMessage).ToList()));
        }

        bool isWeatherStationNameExists = weatherStationRepository.Any(p => p.WeatherStationName == request.WeatherStationName);
        if(isWeatherStationNameExists)
        {
            throw new ArgumentException("Bu istasyon zaten mevcut");
        }

        WeatherStation weatherStation = mapper.Map<WeatherStation>(request);
        weatherStation.CreatedBy = "Admin";
        weatherStation.CreatedDate = DateTime.Now;

        weatherStationRepository.Create(weatherStation);
        return "Hava durumu tahmin istasyonu kaydı oluşturuldu";
    }

    public string DeleteById(Guid id)
    {
        weatherStationRepository.DeleteById(id);
        return "Hava durumu tahmin istasyonu silindi";
    }

    public List<WeatherStation> GetAll()
    {
        List<WeatherStation> weatherStations = weatherStationRepository.GetAll().OrderBy(p => p.WeatherStationName).ToList();
        return weatherStations;
    }

    public string Update(UpdateWeatherStationDto request)
    {
        UpdateWeatherStationDtoValidator validator = new();
        ValidationResult result = validator.Validate(request);
        if(!result.IsValid)
        {
            throw new ArgumentException(string.Join(", ", result.Errors.Select(s => s.ErrorMessage).ToList()));
        }

        WeatherStation? weatherStation = weatherStationRepository.GetWeatherStationById(request.Id);
        if(weatherStation is null)
        {
            throw new ArgumentException("İstasyon Bulunamadı");
        }

        if (weatherStation.WeatherStationName != request.WeatherStationName)
        {
            bool isWeatherStationNameExists = weatherStationRepository.Any(p => p.WeatherStationName == request.WeatherStationName);
            if (isWeatherStationNameExists)
            {
                throw new ArgumentException("Bu istasyon zaten mevcut");
            }
        }

        mapper.Map(request, weatherStation);
        weatherStation.UpdatedBy = "Admin";
        weatherStation.UpdatedDate = DateTime.Now;
        hubContext.Clients.All.SendAsync("WeatherStation", weatherStation);
        weatherStationRepository.Update(weatherStation);
        return "İstasyon bilgisi güncellendi";
    }
}
