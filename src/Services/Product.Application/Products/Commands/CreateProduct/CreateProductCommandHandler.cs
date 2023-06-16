using MediatR;
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

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.ProduceDate, request.ManufacturePhone,
                request.ManufactureEmail, request.IsAvailable);

            var productCreatedEvent = new ProductCreatedEvent(product.Id,product.Name,product.ProduceDate,
                product.ManufacturePhone,product.ManufactureEmail,product.IsAvailable);

            product.AddDomainEvent(productCreatedEvent);

            _productRepository.Add(product);

           return await _productRepository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);
        }
    }
}
