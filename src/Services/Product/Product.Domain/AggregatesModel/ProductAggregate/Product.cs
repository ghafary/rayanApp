using ProductApp.Domain.Events;
using ProductApp.Domain.SeedWork;

namespace ProductApp.Domain.AggregatesModel.ProductAggregate
{
    public class Product: Entity, IAggregateRoot
    {

        protected Product()
        {
        }

        public Product(string identityGuid,string name, DateTime produceDate,string manufacturePhone,
            string manufactureEmail,bool isAvailable) : this()
        {
            IdentityGuid = !string.IsNullOrWhiteSpace(identityGuid) ? identityGuid : throw new ArgumentNullException(nameof(identityGuid));
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
            ProduceDate = produceDate!=DateTime.MinValue && produceDate!=DateTime.MaxValue ? produceDate
                : throw new ArgumentNullException(nameof(produceDate));
            ManufacturePhone= !string.IsNullOrWhiteSpace(manufacturePhone) ? manufacturePhone :
                throw new ArgumentNullException(nameof(manufacturePhone));
            ManufactureEmail = !string.IsNullOrWhiteSpace(manufactureEmail) ? manufactureEmail :
                    throw new ArgumentNullException(nameof(manufactureEmail));
            IsAvailable=isAvailable;

            AddDomainEvent(new ProductCreatedEvent(IdentityGuid, name, produceDate,
                manufacturePhone, manufactureEmail,isAvailable));
        }

        public string IdentityGuid { get; private set; }

        public string Name { get; private set; }

        public DateTime ProduceDate { get; private set; }

        public string ManufacturePhone { get; private set; }

        public string ManufactureEmail { get; private set; }

        public bool IsAvailable { get; private set; }


        public void UpdateProduct(string name, DateTime produceDate, string manufacturePhone,
            string manufactureEmail, bool isAvailable)
        {
            Name = name;
            ProduceDate = produceDate;
            ManufacturePhone = manufacturePhone;
            ManufactureEmail = manufactureEmail;
            IsAvailable = isAvailable;
        }
    }
}
