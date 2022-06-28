using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public interface IApplicationDbContext
{
    DbSet<CustomerEntity>? Customers { get; set; }
    Task<int> SaveChanges();
}