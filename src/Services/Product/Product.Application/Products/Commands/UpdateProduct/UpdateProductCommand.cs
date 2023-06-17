using MediatR;
using ProductApp.Application.Common.Exceptions;
using ProductApp.Application.Products.Interfaces;

namespace ProductApp.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand : IRequest<bool>
{
    public int Id { get; init; }
    public string Name { get; init; }

    public DateTime ProduceDate { get; init; }

    public string ManufacturePhone { get; init; }

    public string ManufactureEmail { get; init; }

    public bool IsAvailable { get; init; }
}
