using CoffeeMugTask.Persistance;
using CoffeeMugTask.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMugTask.Model;

namespace CoffeeMugTask.Products
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(CmtContext context) : base(context)
        {
        }
    }
}
