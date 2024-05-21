using FluentValidation;
using SmartFarmingV2.Entities.DTOs;

namespace SmartFarmingV2.Business.Validator;
public class CreateProductTypeDtoValidator : AbstractValidator<CreateProductTypeDto>
{
    public CreateProductTypeDtoValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(3);
        RuleFor(p => p.Description).NotEmpty().MaximumLength(250);
    }
}
