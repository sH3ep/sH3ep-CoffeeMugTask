using CoffeeMugTask.Model;
using FluentValidation;

namespace CoffeeMugTask.Products.Validators
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleSet("Update", () =>
            {
                RuleFor(product => product.Id)
                    .NotEmpty();

                RuleFor(product => product.Name)
                    .MaximumLength(100)
                    .NotEmpty();

                RuleFor(product => product.Price)
                    .NotEmpty();
            });

            RuleSet("Add", () =>
            {
                RuleFor(product => product.Name)
                    .MaximumLength(100)
                    .NotEmpty();

                RuleFor(product => product.Price)
                    .NotEmpty();
            });

        }
    }
}