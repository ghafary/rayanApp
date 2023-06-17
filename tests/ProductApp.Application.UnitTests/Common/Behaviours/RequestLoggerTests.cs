using Microsoft.Extensions.Logging;
using Moq;
using ProductApp.Application.Common.Interfaces;
using ProductApp.Application.Products.Commands.CreateProduct;

namespace CleanArchitecture.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateProductCommand>> _logger = null!;
    private Mock<IIdentityService> _identityService = null!;

    public RequestLoggerTests()
    {
        _logger = new Mock<ILogger<CreateProductCommand>>();
        _identityService = new Mock<IIdentityService>();
    }

   
}
