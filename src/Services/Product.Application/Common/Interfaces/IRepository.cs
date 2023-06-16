using ProductApp.Domain.SeedWork;

namespace ProductApp.Application.Common.Interfaces;

public interface IRepository<T> where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}
