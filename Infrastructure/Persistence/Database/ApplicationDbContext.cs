using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : Database, IApplicationDbContext
{

    public ApplicationDbContext()
    {
        Database.EnsureCreated();
    }
    
    public DbSet<CustomerEntity>? Customers { get; set; }
    public new async Task<int> SaveChanges()
    {
        return await base.SaveChangesAsync();
    }

}