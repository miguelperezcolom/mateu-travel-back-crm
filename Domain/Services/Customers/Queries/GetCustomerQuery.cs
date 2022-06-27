using Domain.Customers;

namespace Domain.Services.Customers.Queries;

public class GetCustomerQuery: IQuery<Customer>
{
    private readonly ICustomerRepository _repo;
    private readonly string _customerId;

    internal GetCustomerQuery(ICustomerRepository repo, string customerId)
    {
        _repo = repo;
        _customerId = customerId;
    }


    async Task<Customer> IQuery<Customer>.RunAsync()
    {
        return await _repo.GetAsync(_customerId);
    }
}