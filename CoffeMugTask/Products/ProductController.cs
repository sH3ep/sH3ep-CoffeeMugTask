using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            try
            {
                var products = await _productService.GetAll();
                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
                return Ok(productDtos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get([FromRoute] Guid id)
        {
            try
            {
                var product = await _productService.Get(id);
                var productDto = _mapper.Map<ProductDto>(product);
                return Ok(productDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody]ProductCreateRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var product = _mapper.Map<Product>(request);
                var productId = await _productService.Add(product);
                return Ok(productId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody]ProductUpdateRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var product = _mapper.Map<Product>(request);
                await _productService.Update(product);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute]Guid id)
        {
            try
            {
                await _productService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}