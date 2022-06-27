using Domain.Customers;

namespace Domain.Services;

public interface IQuery<T>
{
    public Task<T> RunAsync();

}