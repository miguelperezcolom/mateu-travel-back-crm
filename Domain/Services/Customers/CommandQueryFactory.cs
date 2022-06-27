using Domain.Customers;
using Domain.Services.Customers.Commands;
using Domain.Services.Customers.Queries;

namespace Domain.Services.Customers;

public class CommandQueryFactory
{
    
    private readonly ICustomerRepository _repo;

    public CommandQueryFactory(ICustomerRepository repo)
    {
        _repo = repo;
    }

    public AddCustomerCommand CreateAddCustomerCommand(Customer customer)
    {
        return new(_repo, customer);
    }

    public DeleteCustomerCommand CreateDeleteCustomerCommand(string customerId)
    {
        return new(_repo, customerId);
    }

    public UpdateCustomerCommand CreateUpdateCustomerCommand(Customer customer)
    {
        return new(_repo, customer);
    }

    public GetCustomerQuery CreateGetCustomerQuery(string customerId)
    {
        return new(_repo, customerId);
    }

    public GetCustomersQuery CreateGetCustomersQuery(int page)
    {
        return new(_repo, page);
    }

}