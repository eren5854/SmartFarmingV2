using Microsoft.AspNetCore.Mvc;
using SmartFarmingV2.Business.Services;
using SmartFarmingV2.Entities.DTOs;

namespace SmartFarmingV2.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class WeatherStationsController(IWeatherStationService weatherStationService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = weatherStationService.GetAll();
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(CreateWeatherStationDto request)
    {
        string message = weatherStationService.Create(request);
        return Ok(new {Message = message});
    }

    [HttpPost]
    public IActionResult Update(UpdateWeatherStationDto request)
    {
        string message = weatherStationService.Update(request);
        return Ok(new { Message = message });
    }

    [HttpGet("{id}")]
    public IActionResult DeleteById(Guid id)
    {
        string message = weatherStationService.DeleteById(id);
        return Ok(new { Message = message });
    }
}
