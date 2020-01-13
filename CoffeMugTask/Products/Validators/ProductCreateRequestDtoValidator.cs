using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMugTask.Products.Dto;
using FluentValidation;

namespace CoffeeMugTask.Products.Validators
{
    public class ProductCreateRequestDtoValidator : AbstractValidator<ProductCreateRequestDto>
    {
        public ProductCreateRequestDtoValidator()
        {
            RuleFor(product => product.Name)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(product => product.Price)
                .GreaterThan(0);
        }
    }
}
