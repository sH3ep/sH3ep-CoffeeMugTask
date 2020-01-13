using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMugTask.Model
{
    public class Product
    {
        public Guid Id { set; get; }

        public string Name { set; get; }
        
        public decimal Price { set; get; }
    }
}
