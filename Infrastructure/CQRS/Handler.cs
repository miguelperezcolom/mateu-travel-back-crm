namespace Domain.Services;

public class Handler: IHandler
{
    public async Task<T> RunQueryAsync<T>(IQuery<T> query)
    {
        return await query.RunAsync();
    }
    
    public async Task RunCommandAsync(ICommand query)
    {
        await query.RunAsync();
    }

}