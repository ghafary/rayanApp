using MediatR;
using ProductApp.Domain.AggregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Domain.Events
{
    public class ProductCreatedEvent : INotification
    {
        public int ProductId { get; }
        public string Name { get;}

        public DateTime ProduceDate { get;}

        public string ManufacturePhone { get;}

        public string ManufactureEmail { get; }

        public bool IsAvailable { get; }

        public ProductCreatedEvent(int productId,string name,DateTime produceDate,
            string manufacturePhone,string manufactureEmail,bool isAvailable)
        {
            ProductId = productId;
            Name = name;
            ProduceDate = produceDate;
            ManufacturePhone = manufacturePhone;
            ManufactureEmail = manufactureEmail;
            IsAvailable=isAvailable;
        }
    }
}
