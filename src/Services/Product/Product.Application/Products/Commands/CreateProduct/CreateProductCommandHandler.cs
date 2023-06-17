using MediatR;
using ProductApp.Application.Common.Interfaces;
using ProductApp.Application.Products.Interfaces;
using ProductApp.Domain.AggregatesModel.ProductAggregate;
using ProductApp.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IIdentityService _identityService;

        public CreateProductCommandHandler(IProductRepository productRepository, IIdentityService identityService)
        {
            _identityService = identityService;
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var userIdentity = _identityService.GetUserIdentity();
            var product = new Product(userIdentity,request.Name, request.ProduceDate, request.ManufacturePhone,
                request.ManufactureEmail, request.IsAvailable);

            _productRepository.Add(product);

           return await _productRepository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);
        }
    }
}
