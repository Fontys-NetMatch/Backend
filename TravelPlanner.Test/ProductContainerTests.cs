using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Data;
using TravelPlanner.BLL;
using TravelPlanner.Domain.Interfaces;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Entities.Translations;
using TravelPlanner.Domain.Models.Request.Product;
using TravelPlanner.DB;

namespace TravelPlanner.Test
{
    [TestClass]
    public class ProductContainerTests
    {
        private Mock<DbManager> _mockDb;
        private ProductContainer _container;

        [TestInitialize]
        public void Setup()
        {
            _mockDb = new Mock<DbManager>();
            _container = new ProductContainer(_mockDb.Object);
        }

        private (Mock<ITable<Product>>, List<Product>) CreateProductTableMock()
        {
            var products = new List<Product>
            {
                new Product 
                { 
                    ID = 1,
                    IsActive = true,
                    Translations = new List<ProductTranslation>
                    {
                        new ProductTranslation
                        {
                            ID = 1,
                            LangIsoCode = "en",
                            Name = "Test Product",
                            Product_ID = 1,
                            IsActive = true
                        }
                    }
                }
            };

            var mockTable = new Mock<ITable<Product>>();
            mockTable.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(products.AsQueryable().Provider);
            mockTable.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(products.AsQueryable().Expression);
            mockTable.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(products.GetEnumerator());

            var loadWithMock = new Mock<ILoadWithQueryable<Product, object>>();
            loadWithMock.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(products.AsQueryable().Provider);
            loadWithMock.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(products.AsQueryable().Expression);
            loadWithMock.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(products.GetEnumerator());

            mockTable.Setup(t => t.LoadWith(It.IsAny<Expression<Func<Product, object>>>()))
                    .Returns(loadWithMock.Object);

            return (mockTable, products);
        }

        [TestMethod]
        public async Task GetAll_ReturnsProductsWithTranslations()
        {
            // Arrange
            var (mockTable, products) = CreateProductTableMock();
            _mockDb.Setup(x => x.Products).Returns(mockTable.Object);

            // Act
            var result = await _container.GetAll();

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].Translations.Count);
            Assert.AreEqual("en", result[0].Translations[0].LangIsoCode);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task GetAll_ThrowsWhenNoProducts()
        {
            // Arrange
            var emptyProducts = new List<Product>();
            var mockTable = new Mock<ITable<Product>>();
            mockTable.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(emptyProducts.GetEnumerator());
            _mockDb.Setup(x => x.Products).Returns(mockTable.Object);

            // Act
            await _container.GetAll();
        }

        [TestMethod]
        public async Task GetById_ReturnsProduct()
        {
            // Arrange
            var (mockTable, products) = CreateProductTableMock();
            _mockDb.Setup(x => x.Products).Returns(mockTable.Object);

            // Act
            var result = await _container.GetById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
    }
}