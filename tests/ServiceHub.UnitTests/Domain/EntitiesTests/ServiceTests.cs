using ServiceHub.Domain.Entities;
using ServiceHub.Domain.Enums;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.UnitTests.Domain.EntitiesTests;

public class ServiceTests
{
    [Fact]
    public void Constructor_Should_SetThePropertiesCorrectly()
    {
        // Arrange
        var title = "Test Service";
        var description = "test service description";
        var price = 100.00m;
        var serviceCategories = new List<ServiceCategory>
        {
            new ServiceCategory("category01","description"),
            new ServiceCategory("category02","description")
        };
        var providerId = Guid.NewGuid();

        //Act
        var service = new Service(title,description,price,serviceCategories,providerId);

        // Assert
        service.Title.ShouldBe(title);
        service.Description.ShouldBe(description);
        service.Price.ShouldBe(price);
        service.Categories.ShouldBe(serviceCategories);
        service.ProviderId.ShouldBe(providerId);
        service.ServiceReviews.ShouldBeEmpty();
        service.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
        service.Status.ShouldBe(EServiceStatus.Active);
    }
}
