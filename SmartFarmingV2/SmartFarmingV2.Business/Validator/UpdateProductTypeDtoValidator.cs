using FluentValidation;
using SmartFarmingV2.Entities.DTOs;

namespace SmartFarmingV2.Business.Validator;
public class UpdateProductTypeDtoValidator : AbstractValidator<UpdateProductTypeDto>
{
    public UpdateProductTypeDtoValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.Name).NotEmpty().MinimumLength(3).MaximumLength(150);
        RuleFor(p => p.Description).NotEmpty().MinimumLength(3).MaximumLength(250);
    }
}
