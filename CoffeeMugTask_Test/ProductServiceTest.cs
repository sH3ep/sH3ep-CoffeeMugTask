using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMugTask.Model;
using CoffeeMugTask.Persistance;
using CoffeeMugTask.Persistance.Repositories.Exceptions;
using CoffeeMugTask.Products;
using Moq;
using NUnit.Framework;

namespace CoffeeMugTask_Test
{
    public class ProductServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        private readonly IProductService _productService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IProductRepository> _mockProductRepository;

        public ProductServiceTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepository = new Mock<IProductRepository>();

            _productService = new ProductService(_mockProductRepository.Object, _mockUnitOfWork.Object);

            _mockUnitOfWork.Setup(uow => uow.Complete()).ReturnsAsync(1);
        }

        private async Task<IEnumerable<Product>> GetTestProducts(int amount)
        {
            var products = new List<Product>();
            for (int i = 0; i < amount; i++)
            {
                products.Add(new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Test" + i,
                    Price = Convert.ToDecimal(i)
                });
            }

            return products;
        }
       
        [Test]
        public async Task GetAll()
        {
            var testProducts =await GetTestProducts(5);

            _mockProductRepository.Setup(repo => repo.GetAll()).ReturnsAsync(testProducts);
            

            var products = await _productService.GetAll();

            CollectionAssert.AreEqual(testProducts,products);

        }

        [Test]
        public async Task Get_GoodIdHasBeenGiven()
        {
            var testProducts =await GetTestProducts(1);
            var testProduct = testProducts.ElementAt(0);

            _mockProductRepository.Setup(repo => repo.Get(testProduct.Id)).ReturnsAsync(testProduct);

            var product = await _productService.Get(testProduct.Id);

            Assert.AreSame(testProduct,product);
        }

        [Test]
        public async Task Get_WrongIdHasBeenGiven()
        {
            var testProducts = await GetTestProducts(1);
            var testProduct = testProducts.ElementAt(0);

            _mockProductRepository.Setup(repo => repo.Get(testProduct.Id)).ReturnsAsync((Product) null);

            var e = Assert.ThrowsAsync<EntityNotFoundException>(async()=> await _productService.Get(Guid.NewGuid()));

            Assert.That(e.Message.Equals("Product with that Id does not exists"));


        }
    }
}