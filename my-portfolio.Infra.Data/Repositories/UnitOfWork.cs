using Domain.Interfaces.Repositories;
using Domain.Models.Entities;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private bool _disposed = false;
    private readonly DefaultDbContext  _context;

    private IRepository<Product>? _productRepository;
    private IRepository<Category>? _categoryRepository;
    private IRepository<Order>? _orderRepository;

    public UnitOfWork(DefaultDbContext  context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IRepository<Product> ProductRepository => 
        _productRepository ??= new Repository<Product>(_context);

    public IRepository<Category> CategoryRepository => 
        _categoryRepository ??= new Repository<Category>(_context);

    public IRepository<Order> OrderRepository => 
        _orderRepository ??= new Repository<Order>(_context);

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            _context.Dispose();
        }
        _disposed = true;
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }
}