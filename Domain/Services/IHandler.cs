namespace Domain.Services;

public interface IHandler
{
    public Task<T> RunQueryAsync<T>(IQuery<T> query);

    public Task RunCommandAsync(ICommand query);

}