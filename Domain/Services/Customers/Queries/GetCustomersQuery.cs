using Domain.Customers;

namespace Domain.Services.Customers.Queries;

public class GetCustomersQuery: IQuery<List<Customer>>
{
    
    private readonly ICustomerRepository _repo;
    private readonly int _page;

    internal GetCustomersQuery(ICustomerRepository repo, int page)
    {
        _repo = repo;
        this._page = page;
    }

    async Task<List<Customer>> IQuery<List<Customer>>.RunAsync()
    {
        return await _repo.GetAllAsync(_page);
    }
}