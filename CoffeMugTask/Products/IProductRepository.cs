using CoffeeMugTask.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMugTask.Model;

namespace CoffeeMugTask.Products
{
    public interface IProductRepository:IRepository<Product>
    {
    }
}
