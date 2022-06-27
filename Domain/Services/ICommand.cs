namespace Domain.Services;

public interface ICommand
{
    public Task RunAsync();

}