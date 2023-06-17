using MediatR;
using ProductApp.Application.Common.Exceptions;
using ProductApp.Application.Common.Interfaces;
using ProductApp.Application.Products.Interfaces;
using ProductApp.Domain.AggregatesModel.ProductAggregate;
using ProductApp.Domain.Events;

namespace ProductApp.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(int Id) : IRequest<bool>;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand,bool>
{
    private readonly IProductRepository _productRepository;
    private readonly IIdentityService _identityService;

    public DeleteProductCommandHandler(IProductRepository productRepository,
        IIdentityService identityService)
    {
        _identityService=identityService;
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository
            .GetAsync(request.Id, cancellationToken);

        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        var userIdentity= _identityService.GetUserIdentity();
        if (userIdentity != product.IdentityGuid)
        {
            throw new ForbiddenAccessException();
        }
        product.AddDomainEvent(new ProductDeletedEvent(product));

        _productRepository.Delete(product);
        return await _productRepository.UnitOfWork
            .SaveEntitiesAsync(cancellationToken);
    }

}
