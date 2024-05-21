using AutoMapper;
using SmartFarmingV2.Entities.DTOs;
using SmartFarmingV2.Entities.Models;

namespace SmartFarmingV2.Business.Mapping;
public sealed class MappingProfile :Profile
{
    public MappingProfile()
    {
        CreateMap<CreateProductTypeDto, ProductType>();
        CreateMap<UpdateProductTypeDto, ProductType>();

        CreateMap<CreateWeatherStationDto, WeatherStation>();
        CreateMap<UpdateWeatherStationDto, WeatherStation>();

        CreateMap<CreateSensorDto, Sensor>();
        CreateMap<UpdateSensorDto, Sensor>();

        CreateMap<CreateWeatherForecastLogDto, WeatherForecastLog>();
    }
}
