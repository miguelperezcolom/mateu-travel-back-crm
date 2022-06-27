using Domain.Customers;

namespace Domain.Services.Customers.Commands;

public class AddCustomerCommand : ICommand
{

    private readonly ICustomerRepository _repo;
    private readonly Customer _customer;

    internal AddCustomerCommand(ICustomerRepository repo, Customer customer)
    {
        _repo = repo;
        _customer = customer;
    }

    async Task ICommand.RunAsync()
    {
        await _repo.SaveAsync(_customer);
    }
}