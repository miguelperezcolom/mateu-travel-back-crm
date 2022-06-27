namespace Domain.Customers;

public interface ICustomerRepository
{
    Task SaveAsync(Customer customer);
    Task DeleteAsync(string customerId);
    Task<Customer> GetAsync(string customerId);
    Task<List<Customer>> GetAllAsync(int page);
}