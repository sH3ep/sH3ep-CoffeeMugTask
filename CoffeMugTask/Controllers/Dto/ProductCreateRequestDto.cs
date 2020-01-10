using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMugTask.Controllers.Dto
{
    public class ProductCreateRequestDto
    {
        public string Name { set; get; }

        public decimal Price { set; get; }
    }
}
