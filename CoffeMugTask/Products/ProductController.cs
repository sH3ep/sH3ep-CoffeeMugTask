using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoffeeMugTask.Model;
using CoffeeMugTask.Products.Dto;

namespace CoffeeMugTask.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("")]
        public async Task<ICollection<Product>> Get()
        {
            return null;
        }


        [HttpGet("{id}")]
        public async Task<Product> Get([FromRoute] Guid id)
        {
            return new Product();
        }


        [HttpPost("")]
        public async Task<Guid> Post([FromBody]ProductCreateRequestDto request)
        {

            return new Guid();

        }

        [HttpPut("")]
        public async Task Put([FromBody]ProductUpdateRequestDto request)
        {

        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]Guid id)
        {

        }

    }
}