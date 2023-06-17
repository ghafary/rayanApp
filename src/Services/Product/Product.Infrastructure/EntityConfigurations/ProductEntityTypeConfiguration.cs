using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductApp.Domain.AggregatesModel.ProductAggregate;

namespace ProductApp.Infrastructure.EntityConfigurations
{
    class ProductEntityTypeConfiguration
        : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> productConfiguration)
        {
            productConfiguration.ToTable("Product", ProductContext.DEFAULT_SCHEMA);

            productConfiguration.HasKey(o => o.Id);

            productConfiguration.Ignore(b => b.DomainEvents);

            productConfiguration.Property(o => o.Id)
                .UseHiLo("productitemseq");

            productConfiguration.Property(o=>o.ManufacturePhone)
                .HasColumnType("varchar(50)");

            productConfiguration.Property(o => o.ManufactureEmail)
                .HasColumnType("varchar(200)");

            productConfiguration.Property(o => o.Name)
                .HasColumnType("varchar(200)").IsRequired();

            productConfiguration.Property(o => o.ProduceDate)
                .HasPrecision(3).IsRequired();

            productConfiguration
                .HasIndex(p => new { p.ManufacturePhone, p.ProduceDate })
                .IsUnique(true);
        }
    }

}
