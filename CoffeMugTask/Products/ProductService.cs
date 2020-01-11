using CoffeeMugTask.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMugTask.Products.Dto;
using CoffeeMugTask.Model;
using CoffeeMugTask.Persistance.Repositories.Exceptions;

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

        public async Task<Guid> Add(Product product)
        {
            _productRepository.Add(product);
            await _unitOfWork.Complete();
            return product.Id;
        }

        public async Task Delete(Guid id)
        {
            var product = await _productRepository.Get(id);

            if (product != null)
            {
                _productRepository.Remove(product);
                await _unitOfWork.Complete();
            }
            else
            {
                throw new EntityNotFoundException("Product to delete not fount");
            }
        }

        public async Task<Product> Get(Guid id)
        {
            var product = await _productRepository.Get(id);

            if (product != null)
            {
                return product;
            }

            throw new EntityNotFoundException("Product with that Id does not exists");
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _productRepository.GetAll();

            if (products.Any())
            {
                return products;
            }

            throw new EntityNotFoundException("There are no any product");
        }
    

        public async Task Update(Product product)
        {
            if (!await DoesProductExist(product.Id))
            {
                throw new EntityNotFoundException("There are no any product to update");
            }

            _productRepository.Update(product);
            await _unitOfWork.Complete();
        }


        private async Task<bool> DoesProductExist(Guid id)
        {
            if (await _productRepository.Get(id) is null)
            {
                return false;
            }

            return true;
        }
    }
}
