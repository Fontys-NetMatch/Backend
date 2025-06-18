using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelPlanner.BLL.Container;
using TravelPlanner.Domain.Enums;
using TravelPlanner.Domain.Interfaces.PDF;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Request.Quotation;
using TravelPlanner.DB.Interfaces; // Add this using

namespace ServicesTests.Unit
{
    [TestFixture]
    public class QuotationContainerTest
    {
        private Mock<IQuotationRepository> _mockRepo; // Change to interface
        private Mock<IPDFService> _mockPdfService;
        private QuotationContainer _container;

        [SetUp]
        public void SetUp()
        {
            _mockRepo = new Mock<IQuotationRepository>(); // Change to interface
            _mockPdfService = new Mock<IPDFService>();
            _container = new QuotationContainer(_mockRepo.Object, _mockPdfService.Object);
        }

        #region CreateQuotation Tests

        [Test]
        public void CreateQuotation_NullData_ThrowsArgumentNullException()
        {
            var ex = Assert.ThrowsAsync<ArgumentNullException>(async () => await _container.CreateQuotation(null, 1));
            Assert.AreEqual("Value cannot be null. (Parameter 'data')", ex.Message);
        }

        [Test]
        public void CreateQuotation_EmptyName_ThrowsArgumentException()
        {
            var data = new QuotationData
            {
                Name = "",
                CustomerId = 1
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _container.CreateQuotation(data, 1));
            Assert.AreEqual("Quotation name is required", ex.Message);
        }

        [Test]
        public void CreateQuotation_WhitespaceName_ThrowsArgumentException()
        {
            var data = new QuotationData
            {
                Name = "   ",
                CustomerId = 1
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _container.CreateQuotation(data, 1));
            Assert.AreEqual("Quotation name is required", ex.Message);
        }

        [Test]
        public void CreateQuotation_NullName_ThrowsArgumentException()
        {
            var data = new QuotationData
            {
                Name = null,
                CustomerId = 1
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _container.CreateQuotation(data, 1));
            Assert.AreEqual("Quotation name is required", ex.Message);
        }

        [Test]
        public void CreateQuotation_InvalidCustomerId_ThrowsArgumentException()
        {
            var data = new QuotationData
            {
                Name = "Test Quotation",
                CustomerId = 0
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _container.CreateQuotation(data, 1));
            Assert.AreEqual("Invalid Customer Id", ex.Message);
        }

        [Test]
        public void CreateQuotation_NegativeCustomerId_ThrowsArgumentException()
        {
            var data = new QuotationData
            {
                Name = "Test Quotation",
                CustomerId = -1
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _container.CreateQuotation(data, 1));
            Assert.AreEqual("Invalid Customer Id", ex.Message);
        }

        [Test]
        public void CreateQuotation_InvalidUserId_ThrowsArgumentException()
        {
            var data = new QuotationData
            {
                Name = "Test Quotation",
                CustomerId = 1
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _container.CreateQuotation(data, 0));
            Assert.AreEqual("Invalid User Id", ex.Message);
        }

        [Test]
        public void CreateQuotation_NegativeUserId_ThrowsArgumentException()
        {
            var data = new QuotationData
            {
                Name = "Test Quotation",
                CustomerId = 1
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _container.CreateQuotation(data, -1));
            Assert.AreEqual("Invalid User Id", ex.Message);
        }

        [Test]
        public async Task CreateQuotation_ValidData_ReturnsId()
        {
            var data = new QuotationData
            {
                Name = "Test Quotation",
                CustomerId = 5
            };

            _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Quotation>())).ReturnsAsync(10);

            var result = await _container.CreateQuotation(data, 3);

            Assert.AreEqual(10, result);
            _mockRepo.Verify(r => r.CreateAsync(It.Is<Quotation>(q =>
                q.Name == "Test Quotation" &&
                q.CustomerId == 5 &&
                q.UserId == 3 &&
                q.Status == QuotationStatus.Open)), Times.Once);
        }

        [Test]
        public void CreateQuotation_RepositoryFails_ThrowsInvalidOperationException()
        {
            var data = new QuotationData
            {
                Name = "Test Quotation",
                CustomerId = 1
            };

            _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Quotation>())).ReturnsAsync(0);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _container.CreateQuotation(data, 1));
            Assert.AreEqual("Failed to create quotation", ex.Message);
        }

        #endregion

        #region GetQuotationById Tests

        [Test]
        public void GetQuotationById_InvalidId_ThrowsArgumentException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _container.GetQuotationById(0));
            Assert.AreEqual("Invalid quotation Id", ex.Message);
        }

        [Test]
        public void GetQuotationById_NegativeId_ThrowsArgumentException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _container.GetQuotationById(-1));
            Assert.AreEqual("Invalid quotation Id", ex.Message);
        }

        [Test]
        public async Task GetQuotationById_ValidId_ReturnsQuotation()
        {
            var quotation = new Quotation
            {
                Id = 1,
                Name = "Test Quotation",
                CustomerId = 5,
                UserId = 3,
                Status = QuotationStatus.Open,
                Customer = new Customer { Id = 1, Email = null, Firstname = null, Surname = null },
                User = new User { Id = 1, Email = null, Firstname = null, Password = null, ProfileImagePath = null, Surname = null, IsActive = true }
            };

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(quotation);

            var result = await _container.GetQuotationById(1);

            Assert.NotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Test Quotation", result.Name);
            Assert.AreEqual(QuotationStatus.Open, result.Status);
        }

        [Test]
        public async Task GetQuotationById_NotFound_ReturnsNull()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Quotation)null);

            var result = await _container.GetQuotationById(1);

            Assert.IsNull(result);
        }

        #endregion

        #region GetAllQuotations Tests

        [Test]
        public async Task GetAllQuotations_WithQuotations_ReturnsQuotations()
        {
            var quotations = new List<Quotation>
            {
                new Quotation
                {
                    Id = 1,
                    Name = "Quote 1",
                    Status = QuotationStatus.Open,
                    CustomerId = 1,
                    UserId = 1,
                    Customer = new Customer { Id = 1, Email = null, Firstname  = null, Surname = null },
                    User = new User { Id = 1, Email = null, Firstname = null, Password = null, ProfileImagePath = null, Surname = null, IsActive = true }
                },
                new Quotation
                {
                    Id = 2,
                    Name = "Quote 2",
                    Status = QuotationStatus.Completed,
                    CustomerId = 2,
                    UserId = 2,
                    Customer = new Customer { Id = 1, Email = null, Firstname  = null, Surname = null },
                    User = new User { Id = 1, Email = null, Firstname = null, Password = null, ProfileImagePath = null, Surname = null, IsActive = true }
                }
            };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(quotations);

            var result = await _container.GetAllQuotations();

            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Quote 1", result[0].Name);
            Assert.AreEqual("Quote 2", result[1].Name);
        }

        [Test]
        public void GetAllQuotations_NoQuotations_ThrowsInvalidOperationException()
        {
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Quotation>());

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _container.GetAllQuotations());
            Assert.AreEqual("No quotations found", ex.Message);
        }

        #endregion

        #region GetAllActiveQuotations Tests

        [Test]
        public async Task GetAllActiveQuotations_WithActiveQuotations_ReturnsActiveQuotations()
        {
            var quotations = new List<Quotation>
            {
                new Quotation
                {
                    Id = 1,
                    Name = "Quote 1",
                    Status = QuotationStatus.Open,
                    CustomerId = 1,
                    UserId = 1,
                    Customer = new Customer { Id = 1, Email = null, Firstname  = null, Surname = null },
                    User = new User { Id = 1, Email = null, Firstname = null, Password = null, ProfileImagePath = null, Surname = null, IsActive = true }
                },
                new Quotation
                {
                    Id = 2,
                    Name = "Quote 2",
                    Status = QuotationStatus.Completed,
                    CustomerId = 2,
                    UserId = 2,
                    Customer = new Customer { Id = 1, Email = null, Firstname  = null, Surname = null },
                    User = new User { Id = 1, Email = null, Firstname = null, Password = null, ProfileImagePath = null, Surname = null, IsActive = true }
                }
            };

            _mockRepo.Setup(r => r.GetAllActiveAsync()).ReturnsAsync(quotations);

            var result = await _container.GetAllActiveQuotations();

            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Quote 1", result[0].Name);
            Assert.AreEqual("Quote 2", result[1].Name);
        }

        [Test]
        public void GetAllActiveQuotations_NoActiveQuotations_ThrowsInvalidOperationException()
        {
            _mockRepo.Setup(r => r.GetAllActiveAsync()).ReturnsAsync(new List<Quotation>());

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _container.GetAllActiveQuotations());
            Assert.AreEqual("No active quotations found", ex.Message);
        }

        #endregion

        #region UpdateQuotation Tests

        [Test]
        public void UpdateQuotation_NullData_ThrowsArgumentException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _container.UpdateQuotation(null));
            Assert.AreEqual("Invalid quotation data", ex.Message);
        }

        [Test]
        public void UpdateQuotation_InvalidId_ThrowsArgumentException()
        {
            var data = new QuotationUpdateData { Id = 0, CustomerId = 1, Name = null, Status = QuotationStatus.Completed };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _container.UpdateQuotation(data));
            Assert.AreEqual("Invalid quotation data", ex.Message);
        }

        [Test]
        public void UpdateQuotation_NegativeId_ThrowsArgumentException()
        {
            var data = new QuotationUpdateData { Id = -1, CustomerId = 1, Name = null, Status = QuotationStatus.Completed };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _container.UpdateQuotation(data));
            Assert.AreEqual("Invalid quotation data", ex.Message);
        }

        [Test]
        public void UpdateQuotation_QuotationNotFound_ThrowsInvalidOperationException()
        {
            var data = new QuotationUpdateData { Id = 1, CustomerId = 1, Name = null, Status = QuotationStatus.Completed };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Quotation)null);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _container.UpdateQuotation(data));
            Assert.AreEqual("Quotation does not exist", ex.Message);
        }

        [Test]
        public void UpdateQuotation_RepositoryUpdateFails_ThrowsInvalidOperationException()
        {
            var existing = new Quotation
            {
                Id = 1,
                Name = "Old",
                CustomerId = 1,
                Status = QuotationStatus.Open,
                UserId = 1,
                Customer = new Customer { Id = 1, Email = null, Firstname = null, Surname = null },
                User = new User { Id = 1, Email = null, Firstname = null, Password = null, ProfileImagePath = null, Surname = null, IsActive = true }
            };
            var data = new QuotationUpdateData
            {
                Id = 1,
                Name = "Updated",
                CustomerId = 2,
                Status = QuotationStatus.Completed
            };

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
            _mockRepo.Setup(r => r.UpdateAsync(existing)).ReturnsAsync(0);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _container.UpdateQuotation(data));
            Assert.AreEqual("Failed to update quotation", ex.Message);
        }

        [Test]
        public async Task UpdateQuotation_ValidData_UpdatesQuotation()
        {
            var existing = new Quotation
            {
                Id = 1,
                Name = "Old Name",
                CustomerId = 1,
                Status = QuotationStatus.Open,
                UserId = 1,
                Customer = new Customer { Id = 1, Email = null, Firstname = null, Surname = null },
                User = new User { Id = 1, Email = null, Firstname = null, Password = null, ProfileImagePath = null, Surname = null, IsActive = true }
            };

            var data = new QuotationUpdateData
            {
                Id = 1,
                Name = "Updated Name",
                CustomerId = 5,
                Status = QuotationStatus.Completed
            };

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
            _mockRepo.Setup(r => r.UpdateAsync(existing)).ReturnsAsync(1);

            await _container.UpdateQuotation(data);

            Assert.AreEqual("Updated Name", existing.Name);
            Assert.AreEqual(5, existing.CustomerId);
            Assert.AreEqual(QuotationStatus.Completed, existing.Status);
            _mockRepo.Verify(r => r.UpdateAsync(existing), Times.Once);
        }

        #endregion

        #region SoftDeleteQuotation Tests

        [Test]
        public void SoftDeleteQuotation_QuotationNotFound_ThrowsInvalidOperationException()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Quotation)null);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _container.SoftDeleteQuotation(1));
            Assert.AreEqual("Quotation does not exist", ex.Message);
        }

        [Test]
        public void SoftDeleteQuotation_AlreadyArchived_ThrowsInvalidOperationException()
        {
            var quotation = new Quotation
            {
                Id = 1,
                Status = QuotationStatus.Archived,
                Name = "Test Quotation",
                CustomerId = 1,
                UserId = 1,
                Customer = new Customer { Id = 1, Email = null, Firstname = null, Surname = null },
                User = new User { Id = 1, Email = null, Firstname = null, Password = null, ProfileImagePath = null, Surname = null, IsActive = true }
            };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(quotation);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _container.SoftDeleteQuotation(1));
            Assert.AreEqual("Quotation already archived", ex.Message);
        }

        [Test]
        public void SoftDeleteQuotation_RepositoryUpdateFails_ThrowsInvalidOperationException()
        {
            var quotation = new Quotation
            {
                Id = 1,
                Status = QuotationStatus.Open,
                Name = "Test Quotation",
                CustomerId = 1,
                UserId = 1,
                Customer = new Customer { Id = 1, Email = null, Firstname = null, Surname = null },
                User = new User { Id = 1, Email = null, Firstname = null, Password = null, ProfileImagePath = null, Surname = null, IsActive = true }
            };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(quotation);
            _mockRepo.Setup(r => r.UpdateAsync(quotation)).ReturnsAsync(0);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _container.SoftDeleteQuotation(1));
            Assert.AreEqual("Failed to archive quotation", ex.Message);
        }

        [Test]
        public async Task SoftDeleteQuotation_ValidQuotation_ArchivesQuotation()
        {
            var quotation = new Quotation
            {
                Id = 1,
                Status = QuotationStatus.Open,
                Name = "Test Quotation",
                CustomerId = 1,
                UserId = 1,
                Customer = new Customer { Id = 1, Email = null, Firstname = null, Surname = null },
                User = new User { Id = 1, Email = null, Firstname = null, Password = null, ProfileImagePath = null, Surname = null, IsActive = true }
            };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(quotation);
            _mockRepo.Setup(r => r.UpdateAsync(quotation)).ReturnsAsync(1);

            await _container.SoftDeleteQuotation(1);

            Assert.AreEqual(QuotationStatus.Archived, quotation.Status);
            _mockRepo.Verify(r => r.UpdateAsync(quotation), Times.Once);
        }

        #endregion

        #region GetQuotationProducts Tests

        [Test]
        public async Task GetQuotationProducts_ValidId_ReturnsProducts()
        {
            var products = new List<ProductDate>
            {
                new ProductDate { Id = 1, Price = 100, ProductId =1 },
                new ProductDate { Id = 2, Price = 200, ProductId = 2 }
            };

            _mockRepo.Setup(r => r.GetQuotationProductsAsync(1)).ReturnsAsync(products);

            var result = await _container.GetQuotationProducts(1);

            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(100.00m, result[0].Price);
            Assert.AreEqual(200.00m, result[1].Price);
        }

        [Test]
        public async Task GetQuotationProducts_NoProducts_ReturnsEmptyList()
        {
            _mockRepo.Setup(r => r.GetQuotationProductsAsync(1)).ReturnsAsync(new List<ProductDate>());

            var result = await _container.GetQuotationProducts(1);

            Assert.NotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        #endregion

        #region GeneratePdfAsync Tests

        [Test]
        public async Task GeneratePdfAsync_QuotationNotFound_ReturnsNull()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Quotation)null);

            var result = await _container.GeneratePdfAsync(1);

            Assert.IsNull(result);
            _mockPdfService.Verify(p => p.GenerateQuotation(It.IsAny<Quotation>()), Times.Never);
        }

        [Test]
        public async Task GeneratePdfAsync_ValidQuotation_ReturnsPdfResult()
        {
            var quotation = new Quotation
            {
                Id = 1,
                Name = "Test Quote",
                Status = QuotationStatus.Open,
                CustomerId = 1,
                UserId = 1,
                Customer = new Customer { Id = 1, Email = null, Firstname = null, Surname = null },
                User = new User { Id = 1, Email = null, Firstname = null, Password = null, ProfileImagePath = null, Surname = null, IsActive = true }
            };
            var pdfResult = new FileContentResult(new byte[] { 1, 2, 3 }, "application/pdf");

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(quotation);
            _mockPdfService.Setup(p => p.GenerateQuotation(quotation)).ReturnsAsync(pdfResult);

            var result = await _container.GeneratePdfAsync(1);

            Assert.NotNull(result);
            Assert.AreEqual(pdfResult, result);
            _mockPdfService.Verify(p => p.GenerateQuotation(quotation), Times.Once);
        }

        [Test]
        public async Task GeneratePdfAsync_PdfServiceReturnsNull_ReturnsNull()
        {
            var quotation = new Quotation
            {
                Id = 1,
                Name = "Test Quote",
                Status = QuotationStatus.Open,
                CustomerId = 1,
                UserId = 1,
                Customer = new Customer { Id = 1, Email = null, Firstname = null, Surname = null },
                User = new User { Id = 1, Email = null, Firstname = null, Password = null, ProfileImagePath = null, Surname = null, IsActive = true }
            };

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(quotation);
            _mockPdfService.Setup(p => p.GenerateQuotation(quotation)).ReturnsAsync((FileContentResult)null);

            var result = await _container.GeneratePdfAsync(1);

            Assert.IsNull(result);
            _mockPdfService.Verify(p => p.GenerateQuotation(quotation), Times.Once);
        }

        #endregion
    }
}