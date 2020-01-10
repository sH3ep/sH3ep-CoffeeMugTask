using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMugTask.Controllers.Dto;
using CoffeeMugTask.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMugTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {


        [HttpGet("")]
        public async Task<ActionResult<ICollection<Product>>> Get()
        {
            return Ok(new List<Product>(){ new Product()});
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get([FromRoute] Guid id)
        {
            return Ok( new Product()) ;
        }


        [HttpPost("")]
        public async Task<ActionResult<Guid>> Post([FromBody]ProductCreateRequestDto request)
        {
         return Ok(new Guid());
        }

        [HttpPut("")]
        public async Task<ActionResult> Put([FromBody]ProductUpdateRequestDto request)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute]Guid id)
        {
            return Ok();
        }

    }
}