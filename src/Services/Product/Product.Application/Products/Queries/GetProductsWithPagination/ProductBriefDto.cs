using ProductApp.Application.Common.Mappings;
using ProductApp.Domain.AggregatesModel.ProductAggregate;

namespace ProductApp.Application.Products.Queries.GetProductsWithPagination;

public record ProductSummary : IMapFrom<Product>
{
    public int Id { get; init; }

    public string Name { get; init; }

    public DateTime ProduceDate { get; init; }

    public string ManufacturePhone { get; init; }

    public string ManufactureEmail { get; init; }

    public bool IsAvailable { get; init; }
}
