using FluentValidation;
using SmartFarmingV2.Entities.DTOs;

namespace SmartFarmingV2.Business.Validator;
public sealed class CreateSensorDtoValidator : AbstractValidator<CreateSensorDto>
{
    public CreateSensorDtoValidator()
    {
        RuleFor(p => p.SensorName).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleFor(p => p.ProductCode).NotEmpty().MinimumLength(6).MaximumLength(6);
    }
}
