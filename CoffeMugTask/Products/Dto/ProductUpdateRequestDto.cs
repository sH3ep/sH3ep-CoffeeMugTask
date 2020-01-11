using System;

namespace CoffeeMugTask.Products.Dto
{
    public class ProductUpdateRequestDto
    {
        public Guid Id { set; get; }

        public string Name { set; get; }

        public decimal Price { set; get; }
    }
}
