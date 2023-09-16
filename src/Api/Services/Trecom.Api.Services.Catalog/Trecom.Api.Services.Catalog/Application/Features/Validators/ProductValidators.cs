using FluentValidation;
using Trecom.Api.Services.Catalog.Application.Features.Commands;
using Trecom.Api.Services.Catalog.Constants;
using Trecom.Api.Services.Catalog.Models.Dtos;

namespace Trecom.Api.Services.Catalog.Application.Features.Validators;

public class CreateProductValidator:AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.CreateProductDto.Name)
            .NotEmpty().WithMessage(ValidatorResponseConstants.EmptyProperty)
            .MaximumLength(100).WithMessage(ValidatorResponseConstants.MaximumLength)
            .MinimumLength(5).WithMessage(ValidatorResponseConstants.MinimumLength)
            ;


        RuleFor(x => x.CreateProductDto.UnitPrice).NotEmpty().WithMessage(ValidatorResponseConstants.EmptyProperty)
            .ExclusiveBetween(10, 100000).WithMessage("Price should be between 10 to 100.000 TL");

    }
}