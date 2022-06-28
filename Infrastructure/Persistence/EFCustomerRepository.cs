using Domain.Customers;

namespace Infrastructure.Persistence;

public class EFCustomerRepository : ICustomerRepository
{

    private readonly IApplicationDbContext _dbContext;


    public EFCustomerRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        
    }

    public async Task SaveAsync(Customer customer)
    {
        var found = _dbContext.Customers.Find(customer.Id);
        if (found == null)
        {
            _dbContext.Customers.Add(Mapper.Map(customer));
        }
        else
        {
            found.Address = customer.Address;
            found.City = customer.City;
            found.Country = customer.Country;
            found.Name = customer.Name;
            //_dbContext.Customers.Update(Mapper.Map(customer));
        }
        await _dbContext.SaveChanges();
    }

    public async Task DeleteAsync(string customerId)
    {
        CustomerEntity found;
        if ((found = _dbContext.Customers.Find(customerId)) != null)
        {
            _dbContext.Customers.Remove(found);
            await _dbContext.SaveChanges();
        }
    }

    public async Task<Customer> GetAsync(string customerId)
    {
        return Mapper.Map(_dbContext.Customers.Find(customerId));
    }

    public async Task<List<Customer>> GetAllAsync(int page)
    {
        return _dbContext.Customers.Select(c => Mapper.Map(c)).ToList();
    }
}

public class Mapper
{
    public static CustomerEntity Map(Customer customer)
    {
        if (customer == null) return null;
        return new CustomerEntity()
        {
            CustomerEntityId = customer.Id,
            Name = customer.Name,
            Address = customer.Address,
            City = customer.City,
            Country = customer.Country
        };
    }

    public static Customer Map(CustomerEntity entity)
    {
        if (entity == null) return null;
        return new Customer
        {
            Id = entity.CustomerEntityId,
            Name = entity.Name,
            Address = entity.Address,
            City = entity.City,
            Country = entity.Country
        };
    }
}