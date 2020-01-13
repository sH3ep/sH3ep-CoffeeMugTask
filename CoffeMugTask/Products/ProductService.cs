using CoffeeMugTask.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMugTask.Model;
using CoffeeMugTask.Products.Exceptions;
using CoffeeMugTask.Products.Validators;
using FluentValidation;

namespace CoffeeMugTask.Products
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Add(Product product)
        {
            if(product is null)
            {
                throw new EntityValidationException("Product can't be null");
            }

            var validator = new ProductValidator();
            var validationResult = validator.Validate(product, ruleSet: "Add");

            if (validationResult.IsValid)
            {
                product =_productRepository.Add(product);
                await _unitOfWork.Complete();
                return product.Id;
            }
            else
            {
                throw new EntityValidationException(validationResult.ToString("----"));
            }

        }

        public async Task Delete(Guid id)
        {
            if (!_productRepository.DoesProductExist(id))
            {
                throw new EntityNotFoundException("Product to delete not found");
            }

            var product = await _productRepository.Get(id);
            _productRepository.Remove(product);
            await _unitOfWork.Complete();
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

            throw new EntityNotFoundException("There are no products");
        }


        public async Task Update(Product product)
        {
            if (product == null || !_productRepository.DoesProductExist(product.Id))
            {
                throw new EntityNotFoundException("There are no any product to update");
            }

            var validator = new ProductValidator();
            var validationResult = validator.Validate(product, ruleSet: "Update");

            if (validationResult.IsValid)
            {
                _productRepository.Update(product);
                await _unitOfWork.Complete();
            }
            else
            {
                throw new EntityValidationException(validationResult.ToString("----"));
            }
        }

    }
}
