using ProductApp.Domain.SeedWork;

namespace ProductApp.Domain.AggregatesModel.ProductAggregate
{
    public class Product: Entity, IAggregateRoot
    {

        protected Product()
        {
        }

        public Product(string name, DateTime produceDate,string manufacturePhone,
            string manufactureEmail,bool isAvailable) : this()
        {
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
            ProduceDate = produceDate!=DateTime.MinValue && produceDate!=DateTime.MaxValue ? produceDate
                : throw new ArgumentNullException(nameof(produceDate));
            ManufacturePhone= !string.IsNullOrWhiteSpace(manufacturePhone) ? manufacturePhone :
                throw new ArgumentNullException(nameof(manufacturePhone));
            ManufactureEmail = !string.IsNullOrWhiteSpace(manufactureEmail) ? manufactureEmail :
                    throw new ArgumentNullException(nameof(manufactureEmail));
            IsAvailable=isAvailable;
        }

        public string Name { get; private set; }

        public DateTime ProduceDate { get; private set; }

        public string ManufacturePhone { get; private set; }

        public string ManufactureEmail { get; private set; }

        public bool IsAvailable { get; private set; }
    }
}
