using AutoMapper;
using MediatR;
using ProductApp.Application.Common.Models;
using ProductApp.Application.Products.Interfaces;

namespace ProductApp.Application.Products.Queries.GetProductsWithPagination;

public record GetProductWithProductIdQuery : IRequest<ProductBriefDto>
{
    public int ProductId { get; init; }
}

public class GetProductWithProductIdQueryHandler : IRequestHandler<GetProductWithProductIdQuery, ProductBriefDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductWithProductIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductBriefDto> Handle(GetProductWithProductIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsync(request.ProductId, cancellationToken);
        return _mapper.Map<ProductBriefDto>(product);
    }
}
