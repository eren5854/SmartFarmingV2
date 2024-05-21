using Hangfire;
using Microsoft.EntityFrameworkCore;
using SmartFarmingV2.Business.Hubs;
using SmartFarmingV2.Business.Mapping;
using SmartFarmingV2.Business.Services;
using SmartFarmingV2.DataAccess.Context;
using SmartFarmingV2.DataAccess.Repositories;
using SmartFarmingV2.WebAPI.Works;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(action =>
{
    action.AddDefaultPolicy(policy =>
    policy
    .AllowAnyHeader()
    .AllowAnyMethod()
    .SetIsOriginAllowed(policy => true)
    .AllowCredentials());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

builder.Services.AddHangfire(configuration => configuration.UseSqlServerStorage(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddTransient<MachineLearningBackgroundService>();
builder.Services.AddTransient<ISensorService, SensorService>();
builder.Services.AddHangfireServer();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});


builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddScoped<IWeatherStationRepository, WeatherStationRepository>();
builder.Services.AddScoped<ISensorRepository, SensorRepository>();
//builder.Services.AddScoped<IWeatherForecastLogRepository, WeatherForecastLogRepository>();

builder.Services.AddScoped<IProductTypeService, ProductTypeService>();
builder.Services.AddScoped<IWeatherStationService, WeatherStationService>();
builder.Services.AddScoped<ISensorService, SensorService>();
//builder.Services.AddScoped<IWeatherForecastLogService, WeatherForecastLogService>();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard();

RecurringJob.AddOrUpdate<MachineLearningBackgroundService>(x => x.Test(), "*/10 * * * * *");
RecurringJob.AddOrUpdate<MachineLearningBackgroundService>(x => x.SaveDatabase(), Cron.MinuteInterval(1));

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.MapHub<SensorHub>("/sensor-hub");
app.MapHub<WeatherStationHub>("/weatherStation-hub");

app.Run();
