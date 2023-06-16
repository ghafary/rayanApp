using MediatR;
using ProductApp.Domain.AggregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Domain.Events
{
    public class ProductDeletedEvent : INotification
    {
        public Product Product { get; }

        public ProductDeletedEvent(Product product)
        {
            Product = product;
        }
    }
}
