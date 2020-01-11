using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoffeeMugTask.Model;
using CoffeeMugTask.Products.Dto;

namespace CoffeeMugTask.Products.MappingProfiles
{
    public class ProductMappingProfile:Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductCreateRequestDto, Product>();
            CreateMap<ProductUpdateRequestDto, Product>();
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductUpdateRequestDto>();
            CreateMap<Product, ProductCreateRequestDto>();
            CreateMap<Product, ProductDto>();
        }
    }
}