

using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using ProductApp.Application.Common.Interfaces;
using ProductApp.Application.Products.Commands.CreateProduct;
using ProductApp.Application.Products.Interfaces;
using ProductApp.Domain.AggregatesModel.ProductAggregate;
using Xunit;

namespace UnitTest.Ordering.Application;

public class NewOrderRequestHandlerTest
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IIdentityService> _identityServiceMock;
    private readonly Mock<IMediator> _mediator;

    public NewOrderRequestHandlerTest()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _identityServiceMock = new Mock<IIdentityService>();
        _mediator = new Mock<IMediator>();
    }

    [Fact]
    public async Task Handle_return_false_if_product_is_not_persisted()
    {
        string identityUserId = "1BA3F7D8-6683-ED11-A036-D85ED35AE08B";
        _productRepositoryMock.Setup(orderRepo => orderRepo.GetAsync(It.IsAny<int>(),CancellationToken.None))
            .Returns(Task.FromResult<Product>(FakeProduct()));

        _productRepositoryMock.Setup(buyerRepo => buyerRepo.UnitOfWork.SaveChangesAsync(default))
            .Returns(Task.FromResult(1));

        _identityServiceMock.Setup(svc => svc.GetUserIdentity()).Returns(identityUserId);

        var LoggerMock = new Mock<ILogger<CreateProductCommandHandler>>();
        //Act
        var handler = new CreateProductCommandHandler( _productRepositoryMock.Object, _identityServiceMock.Object);
        var cltToken = new System.Threading.CancellationToken();
        var result = await handler.Handle(FakeProductRequest(), cltToken);

        //Assert
        Assert.False(result);
    }


    private Product FakeProduct()
    {
        var identityGuid = Guid.NewGuid().ToString();
        var name = "FakeProductName";
        var produceDate = DateTime.Now;
        var manufacturePhone = "02154588555";
        var manufactureEmail = "FakeEmail@gmail.com";
        var isAvailable = true;

        return new Product(identityGuid, name, produceDate, manufacturePhone, manufactureEmail, isAvailable);
    }

    private CreateProductCommand FakeProductRequest()
    {
        return new CreateProductCommand()
        {
            IsAvailable = true,
            Name = "FakeProductName",
            ManufactureEmail = "FakeEmail@gmail.com",
            ManufacturePhone = "02154588555",
            ProduceDate = DateTime.Now
        };
    }
}
