using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RentApp.Controllers;
using RentApp.Repository;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace Test;

    public class ApartmentControllerTests
    {
        [Fact]
        public async Task GetApartments_ReturnsOkResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ApartmentController>>();
            var mockRepository = new Mock<IRealEstateRepository>();
            var controller = new ApartmentController(mockRepository.Object, mockLogger.Object);

            // Act
            var result = await controller.GetApartments();

            // Assert
            Assert.True(result is OkObjectResult);
        }

        [Fact]
        public async Task GetDetailsApartments_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ApartmentController>>();
            var mockRepository = new Mock<IRealEstateRepository>();
            var controller = new ApartmentController(mockRepository.Object, mockLogger.Object);
            var validId = "valid-id";

            // Act
            var result = await controller.GetDetailsApartments(validId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetDetailsApartments_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ApartmentController>>();
            var mockRepository = new Mock<IRealEstateRepository>();
            var controller = new ApartmentController(mockRepository.Object, mockLogger.Object);
            var invalidId = "invalid-id";

            // Act
            var result = await controller.GetDetailsApartments(invalidId);

            // Assert
Assert.IsInstanceOf<NotFoundResult>(result);
        }
}