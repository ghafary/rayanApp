using ProductApp.Application.Common.Mappings;
using ProductApp.Domain.AggregatesModel.ProductAggregate;

namespace ProductApp.Application.Products.Queries.GetProductsWithPagination;

public class ProductBriefDto : IMapFrom<Product>
{
    public int Id { get; init; }
}
