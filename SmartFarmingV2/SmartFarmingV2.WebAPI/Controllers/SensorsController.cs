using Microsoft.AspNetCore.Mvc;
using SmartFarmingV2.Business.Services;
using SmartFarmingV2.Entities.DTOs;

namespace SmartFarmingV2.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class SensorsController(ISensorService sensorService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var response = sensorService.GetAll();
        return Ok(response);
    }

    [HttpPost]
    public IActionResult Create(CreateSensorDto request)
    {
        string message = sensorService.Create(request);
        return Ok(new {Message = message});
    }

    [HttpPost]
    public IActionResult Update(UpdateSensorDto request)
    {
        //BackgroundJob.Enqueue(() => MachineLearningBackgroundService.Test());
        string message = sensorService.Update(request);
        return Ok(new {Message = message});
    }

    [HttpGet("{id}")]
    public IActionResult DeleteById(Guid id)
    {
        string message = sensorService.DeleteById(id);
        return Ok(new {Message = message});
    }
}
