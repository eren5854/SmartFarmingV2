using FluentValidation;
using SmartFarmingV2.Entities.DTOs;

namespace SmartFarmingV2.Business.Validator;
public sealed class UpdateSensorDtoValidator : AbstractValidator<UpdateSensorDto>
{
    public UpdateSensorDtoValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.SensorName).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleFor(p => p.ProductCode).NotEmpty().MinimumLength(6).MaximumLength(6);
    }
}
