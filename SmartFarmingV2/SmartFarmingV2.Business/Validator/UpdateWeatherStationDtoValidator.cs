using FluentValidation;
using SmartFarmingV2.Entities.DTOs;

namespace SmartFarmingV2.Business.Validator;
public class UpdateWeatherStationDtoValidator : AbstractValidator<UpdateWeatherStationDto>
{
    public UpdateWeatherStationDtoValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.WeatherStationName).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleFor(p => p.ProductCode).NotEmpty();
    }
}
