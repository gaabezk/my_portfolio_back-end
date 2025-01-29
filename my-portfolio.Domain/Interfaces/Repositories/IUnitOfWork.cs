using Domain.Models.Entities;

namespace Domain.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    IRepository<Product> ProductRepository { get; }
    IRepository<Category> CategoryRepository { get; }
    IRepository<Order> OrderRepository { get; }
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}