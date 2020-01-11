using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMugTask.Products.Dto
{
    public class ProductDto
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public decimal Price { set; get; }
    }
}
