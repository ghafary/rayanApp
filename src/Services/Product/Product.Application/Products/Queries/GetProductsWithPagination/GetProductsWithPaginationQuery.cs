using AutoMapper;
using MediatR;
using ProductApp.Application.Common.Models;
using ProductApp.Application.Products.Interfaces;

namespace ProductApp.Application.Products.Queries.GetProductsWithPagination;

public record GetProductsWithPaginationQuery : IRequest<PaginatedList<ProductSummary>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, PaginatedList<ProductSummary>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsWithPaginationQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<PaginatedList<ProductSummary>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetProductsAsync(request, cancellationToken);
    }
}
