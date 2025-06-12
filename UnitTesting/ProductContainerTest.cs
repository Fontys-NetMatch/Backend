using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelPlanner.BLL.Container;
using TravelPlanner.DB.Interfaces;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Request.Product;

namespace ServicesTests.Unit
{
    [TestFixture]
    public class ProductContainerTest
    {
        private Mock<IProductRepository> _mockRepo;
        private ProductContainer _container;

        [SetUp]
        public void SetUp()
        {
            _mockRepo = new Mock<IProductRepository>();
            _container = new ProductContainer(_mockRepo.Object);
        }

        #region GetById Tests

        [Test]
        public void GetById_InvalidId_ThrowsArgumentException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _container.GetById(0));
            Assert.AreEqual("Invalid product Id (Parameter 'id')", ex.Message);
        }

        [Test]
        public void GetById_NegativeId_ThrowsArgumentException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _container.GetById(-1));
            Assert.AreEqual("Invalid product Id (Parameter 'id')", ex.Message);
        }

        [Test]
        public async Task GetById_ValidId_ReturnsProduct()
        {
            var product = new Product
            {
                Id = 1,
                StartLocation = "New York",
                EndLocation = "London",
                ProductTypeId = 1
            };

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);

            var result = await _container.GetById(1);

            Assert.NotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("New York", result.StartLocation);
        }

        [Test]
        public async Task GetById_NotFound_ReturnsNull()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Product)null);

            var result = await _container.GetById(1);

            Assert.IsNull(result);
        }

        #endregion

        #region GetAll Tests

        [Test]
        public async Task GetAll_WithValidFilters_ReturnsProducts()
        {
            var filters = new ProductFiltersData { TypeId = 1 };
            var products = new List<Product>
            {
                new Product { Id = 1, StartLocation = "Paris", EndLocation = "Rome" },
                new Product { Id = 2, StartLocation = "Berlin", EndLocation = "Madrid" }
            };

            _mockRepo.Setup(r => r.GetFilteredAsync(filters)).ReturnsAsync(products);

            var result = await _container.GetAll(filters);

            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Paris", result[0].StartLocation);
        }

        [Test]
        public void GetAll_RepositoryReturnsNull_ThrowsInvalidOperationException()
        {
            var filters = new ProductFiltersData();
            _mockRepo.Setup(r => r.GetFilteredAsync(filters)).ReturnsAsync((List<Product>)null);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _container.GetAll(filters));
            Assert.AreEqual("No products found", ex.Message);
        }

        #endregion

        #region Create Tests

        [Test]
        public void Create_NullData_ThrowsArgumentNullException()
        {
            var ex = Assert.ThrowsAsync<ArgumentNullException>(async () => await _container.Create(null));
            Assert.AreEqual("Product data cannot be null (Parameter 'data')", ex.Message);
        }

        [Test]
        public void Create_InvalidProductTypeId_ThrowsArgumentException()
        {
            var data = new ProductData
            {
                StartLocation = "New York",
                EndLocation = "London",
                ProductTypeId = 0
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _container.Create(data));
            Assert.AreEqual("Invalid product type Id (Parameter 'ProductTypeId')", ex.Message);
        }

        [Test]
        public void Create_NegativeProductTypeId_ThrowsArgumentException()
        {
            var data = new ProductData
            {
                StartLocation = "New York",
                EndLocation = "London",
                ProductTypeId = -1
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _container.Create(data));
            Assert.AreEqual("Invalid product type Id (Parameter 'ProductTypeId')", ex.Message);
        }

        [Test]
        public async Task Create_ValidData_ReturnsId()
        {
            var data = new ProductData
            {
                StartLocation = "Tokyo",
                EndLocation = "Osaka",
                ProductTypeId = 2,
                DeletedAt = null
            };

            _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Product>())).ReturnsAsync(5);

            var result = await _container.Create(data);

            Assert.AreEqual(5, result);
            _mockRepo.Verify(r => r.CreateAsync(It.Is<Product>(p =>
                p.StartLocation == "Tokyo" &&
                p.EndLocation == "Osaka" &&
                p.ProductTypeId == 2 &&
                p.DeletedAt == null)), Times.Once);
        }

        [Test]
        public void Create_RepositoryFails_ThrowsInvalidOperationException()
        {
            var data = new ProductData
            {
                StartLocation = "Test",
                EndLocation = "Test",
                ProductTypeId = 1
            };

            _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Product>())).ReturnsAsync(0);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _container.Create(data));
            Assert.AreEqual("Failed to create product", ex.Message);
        }

        #endregion

        #region Update Tests

        [Test]
        public void Update_InvalidId_ThrowsArgumentException()
        {
            var data = new ProductData { ProductTypeId = 1, EndLocation = null, StartLocation = null, DeletedAt = null };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _container.Update(0, data));
            Assert.AreEqual("Invalid product or product type Id", ex.Message);
        }

        [Test]
        public void Update_InvalidProductTypeId_ThrowsArgumentException()
        {
            var data = new ProductData { ProductTypeId = 0, EndLocation = null, StartLocation = null, DeletedAt = null };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _container.Update(1, data));
            Assert.AreEqual("Invalid product or product type Id", ex.Message);
        }

        [Test]
        public void Update_ProductNotFound_ThrowsInvalidOperationException()
        {
            var data = new ProductData { ProductTypeId = 1, EndLocation = null, StartLocation = null, DeletedAt = null };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Product)null);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _container.Update(1, data));
            Assert.AreEqual("Product does not exist", ex.Message);
        }

        [Test]
        public void Update_RepositoryUpdateFails_ThrowsInvalidOperationException()
        {
            var existing = new Product { Id = 1, StartLocation = "Old", ProductTypeId = 1 };
            var data = new ProductData
            {
                StartLocation = "New",
                EndLocation = "Updated",
                ProductTypeId = 2
            };

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
            _mockRepo.Setup(r => r.UpdateAsync(existing)).ReturnsAsync(0);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _container.Update(1, data));
            Assert.AreEqual("Failed to update product", ex.Message);
        }

        [Test]
        public async Task Update_ValidData_UpdatesProduct()
        {
            var existing = new Product
            {
                Id = 1,
                StartLocation = "Old Start",
                EndLocation = "Old End",
                ProductTypeId = 1,
                DeletedAt = null
            };

            var data = new ProductData
            {
                StartLocation = "New Start",
                EndLocation = "New End",
                ProductTypeId = 2,
                DeletedAt = DateTime.Now
            };

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
            _mockRepo.Setup(r => r.UpdateAsync(existing)).ReturnsAsync(1);

            await _container.Update(1, data);

            Assert.AreEqual("New Start", existing.StartLocation);
            Assert.AreEqual("New End", existing.EndLocation);
            Assert.AreEqual(2, existing.ProductTypeId);
            Assert.AreEqual(data.DeletedAt, existing.DeletedAt);
            _mockRepo.Verify(r => r.UpdateAsync(existing), Times.Once);
        }

        #endregion

        #region SoftDelete Tests

        [Test]
        public void SoftDelete_ProductNotFound_ThrowsInvalidOperationException()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Product)null);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _container.SoftDelete(1));
            Assert.AreEqual("Product does not exist", ex.Message);
        }

        [Test]
        public void SoftDelete_ProductAlreadyDeleted_ThrowsInvalidOperationException()
        {
            var product = new Product { Id = 1, DeletedAt = DateTime.Now };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _container.SoftDelete(1));
            Assert.AreEqual("Product is already deleted", ex.Message);
        }

        [Test]
        public async Task SoftDelete_ValidProduct_SetsDeletedAtAndReturnsTrue()
        {
            var product = new Product { Id = 1, DeletedAt = null };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);
            _mockRepo.Setup(r => r.UpdateAsync(product)).ReturnsAsync(1);

            var result = await _container.SoftDelete(1);

            Assert.IsTrue(result);
            Assert.IsNotNull(product.DeletedAt);
            Assert.IsTrue((DateTime.Now - product.DeletedAt.Value).TotalSeconds < 5);
            _mockRepo.Verify(r => r.UpdateAsync(product), Times.Once);
        }

        [Test]
        public async Task SoftDelete_UpdateFails_ReturnsFalse()
        {
            var product = new Product { Id = 1, DeletedAt = null };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);
            _mockRepo.Setup(r => r.UpdateAsync(product)).ReturnsAsync(0);

            var result = await _container.SoftDelete(1);

            Assert.IsFalse(result);
            Assert.IsNotNull(product.DeletedAt);
        }

        #endregion

        #region Restore Tests

        [Test]
        public void Restore_ProductNotFound_ThrowsInvalidOperationException()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Product)null);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _container.Restore(1));
            Assert.AreEqual("Product does not exist", ex.Message);
        }

        [Test]
        public void Restore_ProductNotDeleted_ThrowsInvalidOperationException()
        {
            var product = new Product { Id = 1, DeletedAt = null };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _container.Restore(1));
            Assert.AreEqual("Product is already restored", ex.Message);
        }

        [Test]
        public async Task Restore_ValidDeletedProduct_RestoresAndReturnsTrue()
        {
            var product = new Product { Id = 1, DeletedAt = DateTime.Now };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);
            _mockRepo.Setup(r => r.UpdateAsync(product)).ReturnsAsync(1);

            var result = await _container.Restore(1);

            Assert.IsTrue(result);
            Assert.IsNull(product.DeletedAt);
            _mockRepo.Verify(r => r.UpdateAsync(product), Times.Once);
        }

        [Test]
        public async Task Restore_UpdateFails_ReturnsFalse()
        {
            var product = new Product { Id = 1, DeletedAt = DateTime.Now };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);
            _mockRepo.Setup(r => r.UpdateAsync(product)).ReturnsAsync(0);

            var result = await _container.Restore(1);

            Assert.IsFalse(result);
            Assert.IsNull(product.DeletedAt);
        }

        #endregion
    }
}