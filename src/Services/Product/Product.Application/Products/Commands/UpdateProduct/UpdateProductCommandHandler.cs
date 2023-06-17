using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductApp.Application.Common.Exceptions;
using ProductApp.Application.Common.Interfaces;
using ProductApp.Application.Products.Interfaces;
using ProductApp.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IIdentityService _identityService;

        public UpdateProductCommandHandler(IProductRepository productRepository, IIdentityService identityService)
        {
            _identityService = identityService;
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository
                .GetAsync(request.Id, cancellationToken);

            if (product == null)
            {
                throw new NotFoundException(nameof(product), request.Id);
            }

            var userIdentity = _identityService.GetUserIdentity();
            if (userIdentity != product.IdentityGuid)
            {
                throw new ForbiddenAccessException();
            }

            product.UpdateProduct(request.Name, request.ProduceDate, request.ManufacturePhone,
                request.ManufactureEmail, request.IsAvailable);
            var productUpdatedEvent = new ProductUpdatedEvent(product.Id, product.Name, product.ProduceDate,
           product.ManufacturePhone, product.ManufactureEmail, product.IsAvailable);

            product.AddDomainEvent(productUpdatedEvent);

            _productRepository.Update(product);

            return await _productRepository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);
        }
    }
}
