using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductApp.Application.Common.Interfaces;
using ProductApp.Application.Common.Mappings;
using ProductApp.Application.Common.Models;
using ProductApp.Application.Products.Interfaces;
using ProductApp.Application.Products.Queries.GetProductsWithPagination;
using ProductApp.Domain.AggregatesModel.ProductAggregate;

namespace ProductApp.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductContext _context;
    private readonly IMapper _mapper;

    public IUnitOfWork UnitOfWork => _context;

    public ProductRepository(ProductContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Product Add(Product product)
    {
        return _context.Products.Add(product).Entity;
    }

    public void Delete(Product product)
    { 
        _context.Products.Remove(product);
    }

    public async Task<Product> GetAsync(int productId, CancellationToken cancellationToken)
    {
        var product = await _context
            .Products
            .FirstOrDefaultAsync(o => o.Id == productId);

        if (product == null)
        {
            product = _context
                        .Products
                        .Local
                        .FirstOrDefault(o => o.Id == productId);
        }

        return product;
    }

    public async Task<PaginatedList<ProductSummary>> GetProductsAsync(GetProductsWithPaginationQuery request, 
        CancellationToken cancellationToken)
    {
        return await _context.Products
           .OrderBy(x => x.Name)
           .ProjectTo<ProductSummary>(_mapper.ConfigurationProvider)
           .PaginatedListAsync(request.PageNumber, request.PageSize);
    }

    

    public void Update(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
    }
}
