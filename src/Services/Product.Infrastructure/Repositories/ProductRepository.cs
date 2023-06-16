using Microsoft.EntityFrameworkCore;
using ProductApp.Application.Common.Interfaces;
using ProductApp.Application.Products.Interfaces;
using ProductApp.Domain.AggregatesModel.ProductAggregate;

namespace ProductApp.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public ProductRepository(ProductContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
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

    public void Update(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
    }
}
