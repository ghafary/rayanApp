using FluentAssertions;
using ProductApp.Domain.AggregatesModel.ProductAggregate;
using ProductApp.Domain.Events;
using System.Net;
using Xunit;

namespace CleanArchitecture.Domain.UnitTests;

public class ProductAggregateTest
{
    public ProductAggregateTest()
    { }

    [Fact]
    public void Create_Product_success()
    {
        //Arrange    
        var identityGuid = Guid.NewGuid().ToString();
        var name = "FakeProductName";
        var produceDate = DateTime.Now;
        var manufacturePhone = "02154588555";
        var manufactureEmail = "FakeEmail@gmail.com";
        var isAvailable = true;

        //Act 
        var fakeProduct = new Product(identityGuid, name, produceDate, manufacturePhone, manufactureEmail, isAvailable);

        //Assert
        Assert.NotNull(fakeProduct);
    }

    [Fact]
    public void Invalid_manufacturePhone()
    {
        //Arrange    
        var identityGuid = Guid.NewGuid().ToString();
        var name = "FakeProductName";
        var produceDate = DateTime.Now;
        var manufacturePhone = "";
        var manufactureEmail = "FakeEmail@gmail.com";
        var isAvailable = true;

        //Act - Assert
        Assert.Throws<ArgumentNullException>(() => new Product(identityGuid, name, produceDate, manufacturePhone, manufactureEmail, isAvailable));
    }

    [Fact]
    public void Remove_event_Product_explicitly()
    {
        //Arrange    
        var identityGuid = Guid.NewGuid().ToString();
        var name = "FakeProductName";
        var produceDate = DateTime.Now;
        var manufacturePhone = "";
        var manufactureEmail = "FakeEmail@gmail.com";
        var isAvailable = true;
        var fakeProdut = new Product(identityGuid, name, produceDate, manufacturePhone, manufactureEmail, isAvailable);
        var @fakeEvent = new ProductCreatedEvent(identityGuid, name, produceDate, manufacturePhone, manufactureEmail, isAvailable);
        var expectedResult = 1;

        //Act         
        fakeProdut.AddDomainEvent(@fakeEvent);
        fakeProdut.RemoveDomainEvent(@fakeEvent);
        //Assert
        Assert.Equal(fakeProdut.DomainEvents.Count, expectedResult);
    }
}
