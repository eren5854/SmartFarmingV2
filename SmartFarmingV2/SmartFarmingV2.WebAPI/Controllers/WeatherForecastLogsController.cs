using Microsoft.AspNetCore.Mvc;
using SmartFarmingV2.Business.Services;
using SmartFarmingV2.DataAccess.Context;
using SmartFarmingV2.Entities.DTOs;

namespace SmartFarmingV2.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class WeatherForecastLogsController
    : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public WeatherForecastLogsController(ApplicationDbContext context)
    {
        _context = context;
    }

    //[HttpPost]
    //public IActionResult Create(CreateWeatherForecastLogDto request)
    //{
    //    string message = weatherForecastLogService.Create(request);
    //    return Ok(new { Message = message });
    //}

    [HttpGet]
    public IActionResult Get()
    {
        var result = _context.WeatherForecastLogs.OrderBy(o => o.CreatedDate).ToList();
        return Ok(result);
    }
}
