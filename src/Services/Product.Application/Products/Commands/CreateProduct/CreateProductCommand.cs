using MediatR;
using ProductApp.Application.Products.Interfaces;
using ProductApp.Domain.AggregatesModel.ProductAggregate;

namespace ProductApp.Application.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<bool>
{
    public string Name { get; init; }

    public DateTime ProduceDate { get; init; }

    public string ManufacturePhone { get; init; }

    public string ManufactureEmail { get; private set; }

    public bool IsAvailable { get; init; }
}


