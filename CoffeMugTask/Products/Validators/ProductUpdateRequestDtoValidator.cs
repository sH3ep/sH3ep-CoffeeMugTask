using CoffeeMugTask.Products.Dto;
using FluentValidation;

namespace CoffeeMugTask.Products.Validators
{
    public class ProductUpdateRequestDtoValidator: AbstractValidator<ProductUpdateRequestDto>
    {
        public ProductUpdateRequestDtoValidator()
        {
            RuleFor(product => product.Id)
                .NotEmpty();

            RuleFor(product => product.Name)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(product => product.Price)
                .NotEmpty();
        }
    }
}