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

        [StringLength(100, MinimumLength = 1, ErrorMessage = "Invalid character amount")]
        public string Name { set; get; }
        [Required(ErrorMessage = "Wrong Price value")]
        public decimal Price { set; get; }
    }
}
