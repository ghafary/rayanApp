using MediatR;
using ProductApp.Application.Common.Exceptions;
using ProductApp.Application.Products.Interfaces;

namespace ProductApp.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand : IRequest<bool>
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }
}
