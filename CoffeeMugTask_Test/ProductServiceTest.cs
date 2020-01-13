using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMugTask.Model;
using CoffeeMugTask.Persistance;
using CoffeeMugTask.Products;
using CoffeeMugTask.Products.Exceptions;
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

        #region GetAllTests

        [Test]
        public async Task GetAll()
        {
            var testProducts = await GetTestProducts(5);

            _mockProductRepository.Setup(repo => repo.GetAll()).ReturnsAsync(testProducts);


            var products = await _productService.GetAll();

            CollectionAssert.AreEqual(testProducts, products);

        }


        [Test]
        public async Task GetAll_RepositoryReturnedEmptyCollection()
        {
            var products = new List<Product>();

            _mockProductRepository.Setup(repo => repo.GetAll()).ReturnsAsync(products);

            var e = Assert.ThrowsAsync<EntityNotFoundException>(async () => await _productService.GetAll());

        }

        #endregion

        #region GetTests

        [Test]
        public async Task Get_GoodIdHasBeenGiven()
        {
            var testProducts = await GetTestProducts(1);
            var testProduct = testProducts.ElementAt(0);

            _mockProductRepository.Setup(repo => repo.Get(testProduct.Id)).ReturnsAsync(testProduct);

            var product = await _productService.Get(testProduct.Id);

            Assert.AreSame(testProduct, product);
        }

        [Test]
        public async Task Get_WrongIdHasBeenGiven()
        {
            var testProducts = await GetTestProducts(1);
            var testProduct = testProducts.ElementAt(0);

            _mockProductRepository.Setup(repo => repo.Get(testProduct.Id)).ReturnsAsync((Product)null);

            var e = Assert.ThrowsAsync<EntityNotFoundException>(async () => await _productService.Get(Guid.NewGuid()));

            Assert.That(e.Message.Equals("Product with that Id does not exists"));
        }

        #endregion

        #region AddTests

        [Test]
        public async Task Add_GoodProductHasBeenGiven()
        {
            var id = Guid.NewGuid();
            var testProduct = new Product { Id = id, Name = "Imie", Price = 12.54m };
            var serviceInputProduct = new Product { Name = "Imie", Price = 12.54m };


            _mockProductRepository.Setup(repo => repo.Add(serviceInputProduct)).Returns(new Product { Id = id, Name = "Imie", Price = 12.54m });
            // _mockProductRepository.Setup(repo => repo.Get(testProduct.Id)).ReturnsAsync((Product)null);

            var productId = await _productService.Add(serviceInputProduct);

            Assert.That(id.Equals(productId));
        }

        [Test]
        public async Task Add_ToShortProductNameHasBeenGiven()
        {
            var id = Guid.NewGuid();
            var testProduct = new Product { Id = id, Name = "", Price = 12.54m };


            _mockProductRepository.Setup(repo => repo.Add(new Product() { Name = "Imie", Price = 12.54m })).Returns(testProduct);

            var e = Assert.ThrowsAsync<EntityValidationException>(async () => await _productService.Add(testProduct));
        }

        [Test]
        public async Task Add_ToLongProductNameHasBeenGiven()
        {
            var id = Guid.NewGuid();
            var testProduct = new Product { Id = id, Name = "qwertyuiopwqwertyuiopwqwertyuiopwqwertyuiopwqwertyuiopwqwertyuiopwqwertyuiopwqwertyuiopwqwertyuiopwqwertyuiopwqwertyuiopw", Price = 12.54m };
            var serviceInputProduct = new Product { Name = testProduct.Name, Price = 12.54m };

            _mockProductRepository.Setup(repo => repo.Add(new Product() { Name = testProduct.Name, Price = 12.54m })).Returns(testProduct);

            var e = Assert.ThrowsAsync<EntityValidationException>(async () => await _productService.Add(serviceInputProduct));
        }

        [Test]
        public async Task Add_ProductPriceIsZero()
        {
            var id = Guid.NewGuid();
            var testProduct = new Product { Id = id, Name = "Imie", Price = 0.0m };
            var serviceInputProduct = new Product { Name = "Imie", Price = 0.0m };


            _mockProductRepository.Setup(repo => repo.Add(new Product() { Name = "Imie", Price = 0.0m })).Returns(testProduct);

            var e = Assert.ThrowsAsync<EntityValidationException>(async () => await _productService.Add(serviceInputProduct));
        }

        [Test]
        public async Task Add_ProductPriceIsLessThanZero()
        {
            var id = Guid.NewGuid();
            var testProduct = new Product { Id = id, Name = "Imie", Price = -2.0m };
            var serviceInputProduct = new Product { Name = "Imie", Price = -2.0m };


            _mockProductRepository.Setup(repo => repo.Add(new Product() { Name = "Imie", Price = -2.0m })).Returns(testProduct);

            var e = Assert.ThrowsAsync<EntityValidationException>(async () => await _productService.Add(serviceInputProduct));
        }

        [Test]
        public async Task Add_ProductIsNull()
        {
            var e = Assert.ThrowsAsync<EntityValidationException>(async () => await _productService.Add((Product)null));
        }

        #endregion

        #region UpdateTests

        [Test]
        public async Task Update_CorrectProductHasBeenGiven()
        {
            var id = Guid.NewGuid();
            var testProduct = new Product { Id = id, Name = "Imie", Price = 3.0m };
            var serviceInputProduct = new Product { Id = id, Name = "Imie", Price = 2.0m };

            _mockProductRepository.Setup(repo => repo.DoesProductExist(id)).Returns(true);
            _mockProductRepository.Setup(repo => repo.Update(serviceInputProduct)).Returns(testProduct);

            await _productService.Update(serviceInputProduct);

            Assert.Pass();
        }

        [Test]
        public async Task Update_ProductIdNotExistsInDb()
        {
            var id = Guid.NewGuid();
            var testProduct = new Product { Id = id, Name = "Imie", Price = 3.0m };
            var serviceInputProduct = new Product { Id = id, Name = "Imie", Price = 2.0m };

            _mockProductRepository.Setup(repo => repo.DoesProductExist(id)).Returns(false);

            Assert.ThrowsAsync<EntityNotFoundException>(async () => await _productService.Update(serviceInputProduct));

            Assert.Pass();
        }

        [Test]
        public async Task Update_ProductIdIsZero()
        {
            var id = Guid.Empty;
            var serviceInputProduct = new Product { Id = id, Name = "Imie", Price = 0.0m };
            _mockProductRepository.Setup(repo => repo.DoesProductExist(id)).Returns(true);

            Assert.ThrowsAsync<EntityValidationException>(async () => await _productService.Update(serviceInputProduct));
        }

        [Test]
        public async Task Update_ProductIsNull()
        {
            var id = Guid.Empty;
            _mockProductRepository.Setup(repo => repo.DoesProductExist(id)).Returns(true);
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await _productService.Update(null));
        }

        [Test]
        public async Task Update_ToShortProductNameHasBeenGiven()
        {
            var id = Guid.NewGuid();
            var serviceInputProduct = new Product { Id = id, Name = "", Price = 2.0m };
            _mockProductRepository.Setup(repo => repo.DoesProductExist(id)).Returns(true);

            Assert.ThrowsAsync<EntityValidationException>(async () => await _productService.Update(serviceInputProduct));

        }

        [Test]
        public async Task Update_ToLongProductNameHasBeenGiven()
        {
            var id = Guid.NewGuid();
            var serviceInputProduct = new Product { Id = id, Name = "qwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwerqwer", Price = 2.0m };
            _mockProductRepository.Setup(repo => repo.DoesProductExist(id)).Returns(true);

            Assert.ThrowsAsync<EntityValidationException>(async () => await _productService.Update(serviceInputProduct));

        }

        [Test]
        public async Task Update_ProductNameIsNull()
        {
            var id = Guid.NewGuid();
            var serviceInputProduct = new Product { Id = id, Name = null, Price = 2.0m };
            _mockProductRepository.Setup(repo => repo.DoesProductExist(id)).Returns(true);

            Assert.ThrowsAsync<EntityValidationException>(async () => await _productService.Update(serviceInputProduct));
        }

        [Test]
        public async Task Update_ProductPriceIsZero()
        {
            var id = Guid.NewGuid();
            var serviceInputProduct = new Product { Id = id, Name = "Imie", Price = 0.0m };
            _mockProductRepository.Setup(repo => repo.DoesProductExist(id)).Returns(true);

            Assert.ThrowsAsync<EntityValidationException>(async () => await _productService.Update(serviceInputProduct));
        }
        [Test]
        public async Task Update_ProductPriceIsLessThanZero()
        {
            var id = Guid.NewGuid();
            var serviceInputProduct = new Product { Id = id, Name = "Imie", Price = -10.0m };
            _mockProductRepository.Setup(repo => repo.DoesProductExist(id)).Returns(true);

            Assert.ThrowsAsync<EntityValidationException>(async () => await _productService.Update(serviceInputProduct));
        }


        #endregion

        #region DeleteTests

        [Test]
        public async Task Delete_ProductToDeleteExists()
        {
            var id = Guid.NewGuid();
            var testProduct = new Product { Id = id, Name = "Imie", Price = 3.0m };
            var serviceInputProduct = new Product { Id = id, Name = "Imie", Price = 2.0m };

            _mockProductRepository.Setup(repo => repo.DoesProductExist(id)).Returns(true);
            _mockProductRepository.Setup(repo => repo.Get(id)).ReturnsAsync(testProduct);
            _mockProductRepository.Setup(repo => repo.Remove(testProduct));

            await _productService.Delete(id);

            Assert.Pass();
        }

        [Test]
        public async Task Delete_ProductToDeleteDoesNotExists()
        {
            var id = Guid.NewGuid();
            var testProduct = new Product { Id = id, Name = "Imie", Price = 3.0m };

            _mockProductRepository.Setup(repo => repo.DoesProductExist(id)).Returns(false);
            _mockProductRepository.Setup(repo => repo.Get(id)).ReturnsAsync(testProduct);
            _mockProductRepository.Setup(repo => repo.Remove(testProduct));

            Assert.ThrowsAsync<EntityNotFoundException>(async () => await _productService.Delete(id));

  
        }

        #endregion





    }
}