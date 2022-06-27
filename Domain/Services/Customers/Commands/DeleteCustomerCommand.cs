using Domain.Customers;

namespace Domain.Services.Customers.Commands;

public class DeleteCustomerCommand : ICommand
{
    private readonly ICustomerRepository _repo;
    private readonly string _customerId;

    internal DeleteCustomerCommand(ICustomerRepository repo, string customerId)
    {
        _repo = repo;
        _customerId = customerId;
    }

    async Task ICommand.RunAsync()
    {
        await _repo.DeleteAsync(_customerId);
    }
}