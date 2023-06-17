using ProductApp.Application.Common.Interfaces;
using ProductApp.Application.Common.Models;
using ProductApp.Application.Products.Queries.GetProductsWithPagination;
using ProductApp.Domain.AggregatesModel.ProductAggregate;

namespace ProductApp.Application.Products.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Product Add(Product product);

        void Update(Product product);

        void Delete(Product product);

        Task<Product> GetAsync(int productId,CancellationToken cancellationToken);

        Task<PaginatedList<ProductSummary>> GetProductsAsync(GetProductsWithPaginationQuery request,
        CancellationToken cancellationToken);
    }
}
