using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMugTask.Model;
using CoffeeMugTask.Products.Dto;

namespace CoffeeMugTask.Products
{
    public interface IProductService
    {
        Task<Guid> Add(Product product);
        Task<Product> Get(Guid id);
        Task<IEnumerable<Product>> GetAll();
        Task Update(Product product);
        Task Delete(Guid id);
    }
}
