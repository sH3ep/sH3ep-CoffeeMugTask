using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMugTask.Products.Dto;

namespace CoffeeMugTask.Products
{
    public interface IProductService
    {
        Task<Guid> Add(ProductCreateRequestDto productDto);
    }
}
