using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Contexts;

public class DefaultDbContext(DbContextOptions<DefaultDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
}