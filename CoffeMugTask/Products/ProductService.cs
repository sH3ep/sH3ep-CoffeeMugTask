using CoffeeMugTask.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMugTask.Products.Dto;

namespace CoffeeMugTask.Products
{
    public class ProductService: IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService (IProductRepository productRepository,IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Guid> Add(ProductCreateRequestDto productDto)
        {
            throw new NotImplementedException();
        }
    }
}
