using Domain.Customers;

namespace Infrastructure.Persistence;

public class CustomerRepository : ICustomerRepository
{
    
    private static readonly Dictionary<string, Customer> _data = new Dictionary<string, Customer>();

    
    public async Task SaveAsync(Customer customer)
    {
        _data[customer.Id] = customer;
    }

    public async Task DeleteAsync(string customerId)
    {
        if (_data.ContainsKey(customerId)) _data.Remove(customerId);
    }

    public async Task<Customer> GetAsync(string customerId)
    {
        if (!_data.ContainsKey(customerId)) return null; 
        return _data[customerId];
    }

    public async Task<List<Customer>> GetAllAsync(int page)
    {
        return _data.Select(p => p.Value).ToList();
    }
}