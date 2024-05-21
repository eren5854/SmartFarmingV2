using FluentValidation;
using SmartFarmingV2.Entities.DTOs;

namespace SmartFarmingV2.Business.Validator;
public class CreateWeatherStationDtoValidator : AbstractValidator<CreateWeatherStationDto>
{
    public CreateWeatherStationDtoValidator()
    {
        RuleFor(p => p.WeatherStationName).NotEmpty().MinimumLength(3);
        RuleFor(p => p.ProductCode).NotEmpty();
    }
}
